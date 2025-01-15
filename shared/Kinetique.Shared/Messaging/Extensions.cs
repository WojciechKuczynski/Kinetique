using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Kinetique.Shared.Messaging;

public static class Extensions
{
    private const string ConfigSection = "RabbitMqConnection";
    
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConnection = configuration.GetConnectionString(ConfigSection);
        var factory = new ConnectionFactory { Uri = new Uri(rabbitMqConnection!)};
        // var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();
        services.AddSingleton(connection);
        services.AddSingleton<IRabbitPublisher, RabbitPublisher>();
        services.AddSingleton<IRabbitConsumer, RabbitConsumer>();
        
        return services;
    }
    
    public static IServiceCollection AddRabbitMqRpc(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConnection = configuration.GetConnectionString(ConfigSection);
        var factory = new ConnectionFactory { Uri = new Uri(rabbitMqConnection!)};
        // var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();

        services.AddSingleton(connection);
        services.AddSingleton<IRabbitPublisher, RabbitPublisher>();
        services.AddSingleton<IRabbitRequestWorker, RabbitRequestWorker>();
        
        return services;
    }

    public static IServiceCollection CreateRabbitTopology(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConnection = configuration.GetConnectionString(ConfigSection);
        var factory = new ConnectionFactory { Uri = new Uri(rabbitMqConnection!)};
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        
        channel.ExchangeDeclare("appointment", "direct", durable:true, autoDelete: false);
        
        channel.QueueDeclare("appointment-queue", durable: true, exclusive: false, autoDelete: false);
        channel.QueueDeclare("appointment-finished-queue", durable: true, exclusive: false, autoDelete: false);
        channel.QueueDeclare("patient-details-queue", durable: true, exclusive: false, autoDelete: false);

        return services;
    }
}