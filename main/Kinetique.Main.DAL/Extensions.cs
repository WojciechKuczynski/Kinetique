using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kinetique.Main.DAL;

public static class Extensions
{
    private const string PostgresConnectionStringSection = "PostgresConnection";

    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnectionString = configuration.GetConnectionString(PostgresConnectionStringSection);
        services.AddDbContext<DataContext>(x => x.UseNpgsql(postgresConnectionString).UseLazyLoadingProxies());
        
        // EF Core + Npgsql issue
        // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    } 
}