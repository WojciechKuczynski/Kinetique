using Kinetique.Nfz.Application.Repositories;
using Kinetique.Nfz.DAL.Repositories;
using Kinetique.Shared.Model.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Kinetique.Nfz.Application;

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

        services.AddSingleton<IPatientProcedureRepository, InMemoryPatientProcedureRepository>();
        
        return services;
    }
}