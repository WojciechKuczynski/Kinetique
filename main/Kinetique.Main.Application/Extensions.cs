using Kinetique.Main.Application.Repositories;
using Kinetique.Main.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;
using Kinetique.Shared.Model.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Kinetique.Main.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(Extensions).Assembly;

        services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        
        services.AddScoped<IPatientRepository, InMemoryPatientRepository>()
            .AddScoped<IDoctorRepository, InMemoryDoctorRepository>()
            .AddScoped<IResponseStorage, ResponseStorage>();
        
        return services;
    }
}