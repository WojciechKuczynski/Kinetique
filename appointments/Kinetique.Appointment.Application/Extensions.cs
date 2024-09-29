using Kinetique.Appointment.Application.Repositories;
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

        services.AddSingleton<IAppointmentRepository, InMemoryAppointmentRepository>()
            .AddScoped<IResponseStorage, ResponseStorage>();
        
        return services;
    }
}