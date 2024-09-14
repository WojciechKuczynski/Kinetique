using Kinetique.Main.Application.Abstractions;
using Kinetique.Main.Application.Repositories;
using Kinetique.Main.Application.Storage;
using Kinetique.Main.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Kinetique.Main.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ICommandHandler<>).Assembly;

        services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddScoped<IPatientRepository, InMemoryPatientRepository>()
            .AddScoped<IDoctorRepository, InMemoryDoctorRepository>()
            .AddScoped<IResponseStorage, ResponseStorage>();
        
        return services;
    }
}