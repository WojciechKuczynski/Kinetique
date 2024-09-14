using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kinetique.Main.DAL;

public static class Extensions
{
    private const string PostgresConnectionStringSection = "PostgresConnection";

    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration, string provider)
    {
        var postgresConnectionString = configuration.GetConnectionString(PostgresConnectionStringSection);
        // var sqlServerConnectionString = configuration.GetConnectionString(SqlServerConnectionStringSection);
        // services.AddDbContext<DataContext>(options => _ = provider switch
        // {
        //     "Postgres" => options.UseNpgsql(postgresConnectionString).UseLazyLoadingProxies(),
        //     "SqlServer" => options.UseSqlServer(sqlServerConnectionString).UseLazyLoadingProxies(),
        // });
        services.AddDbContext<DataContext>(x => x.UseNpgsql(postgresConnectionString).UseLazyLoadingProxies());
        
        // EF Core + Npgsql issue
        // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    } 
}