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
        Console.WriteLine("------------------");
        Console.WriteLine(rabbitMqConnection);
        Console.WriteLine("------------------");
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        
        channel.ExchangeDeclare("appointment", "direct", durable:true, autoDelete: false);
        
        channel.QueueDeclare("appointment-finished-queue", durable: true, exclusive: false, autoDelete: false);
        channel.QueueDeclare("appointment-created-queue", durable: true, exclusive: false, autoDelete: false);
        channel.QueueDeclare("appointment-removed-queue", durable: true, exclusive: false, autoDelete: false);
        
        channel.QueueBind("appointment-created-queue", "appointment", "appointment.created");
        channel.QueueBind("appointment-removed-queue", "appointment", "appointment.removed");
        channel.QueueBind("appointment-finished-queue", "appointment", "appointment.finished");

        return services;
    }
}