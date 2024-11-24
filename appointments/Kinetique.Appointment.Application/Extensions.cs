using Kinetique.Appointment.Application.BackgroundWorkers;
using Kinetique.Appointment.Application.Repositories;
using Kinetique.Appointment.Application.Services;
using Kinetique.Appointment.Application.Services.Interfaces;
using Kinetique.Appointment.DAL.Repositories;
using Kinetique.Shared.Messaging;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kinetique.Appointment.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(Extensions).Assembly;

        services.AddRabbitMq(configuration);
        services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddScoped<IAppointmentRepository, PostgresAppointmentRepository>()
            .AddScoped<IAppointmentJournalRepository, PostgresAppointmentJournalRepository>()
            .AddScoped<IResponseStorage, ResponseStorage>()
            .AddScoped<IAppointmentAvailabilityService, AppointmentAvailabilityService>();
        
        services.AddHostedService<AppointmentFinishBackgroundService>(x =>
        {
            var scope = x.CreateScope();
            var appointmentJournalRepository = scope.ServiceProvider.GetRequiredService<IAppointmentJournalRepository>();
            var appointmentRepository = scope.ServiceProvider.GetRequiredService<IAppointmentRepository>();
            var rabbitPublisher = scope.ServiceProvider.GetRequiredService<IRabbitPublisher>();
            return new AppointmentFinishBackgroundService(appointmentJournalRepository, appointmentRepository, rabbitPublisher);
        });
        
        return services;
    }
}