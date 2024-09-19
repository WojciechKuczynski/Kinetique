using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kinetique.Appointment.DAL;

public static class Extensions
{
    private const string PostgresConnectionStringSection = "PostgresConnection";
    
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration, string provider)
    {
        var postgresConnectionString = configuration.GetConnectionString(PostgresConnectionStringSection);
        services.AddDbContext<DataContext>(x => x.UseNpgsql(postgresConnectionString).UseLazyLoadingProxies());
        return services;
    } 
}