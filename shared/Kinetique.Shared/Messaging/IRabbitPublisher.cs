namespace Kinetique.Shared.Messaging;

public interface IRabbitPublisher
{
    void PublishToExchange<TMessage>(TMessage message, string exchange, string routingKey = default) where TMessage : class, IRabbitRequest;
    
    void PublishToQueue<TMessage>(TMessage message, string queue) where TMessage : class, IRabbitRequest;
}