using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Kinetique.Shared.Messaging;

public static class Extensions
{
    private const string ConfigSection = "RabbitMq";
    
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection();

        services.AddSingleton(connection);
        services.AddSingleton<IRabbitPublisher, RabbitPublisher>();
        services.AddSingleton<IRabbitConsumer, RabbitConsumer>();
        
        return services;
    }
    
    public static IServiceCollection AddRabbitMqRpc(this IServiceCollection services, IConfiguration configuration)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection();

        services.AddSingleton(connection);
        services.AddSingleton<IRabbitPublisher, RabbitPublisher>();
        services.AddSingleton<IRabbitRequestWorker, RabbitRequestWorker>();
        
        return services;
    }
}