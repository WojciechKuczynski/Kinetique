using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kinetique.Schedule.DAL;

public static class ExtensionsDAL
{
    private const string PostgresConnectionStringSection = "PostgresConnection";

    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnectionString = configuration.GetConnectionString(PostgresConnectionStringSection);
        services.AddDbContext<DataContext>(x => x.UseNpgsql(postgresConnectionString).UseLazyLoadingProxies());
        return services;
    }     
}