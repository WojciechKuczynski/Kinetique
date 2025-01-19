using Kinetique.Schedule.Repositories;
using Kinetique.Shared.Model.Abstractions;

namespace Kinetique.Schedule;

public static class Extension
{
    public static IServiceCollection AddSchedule(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(Extension).Assembly;
        services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddScoped<IScheduleRepository, PostgresScheduleRepository>();
        
        return services;
    } 
}