namespace Kinetique.Shared.Messaging;

public interface IRabbitPublisher
{
    void PublishToExchange<TMessage>(TMessage message, string exchange, string routingKey = default) where TMessage : class, IRabbitRequest;
    
    void PublishToQueue<TMessage>(TMessage message, string queue) where TMessage : class, IRabbitRequest;
    
    Task<TResponse> PublishRequest<TMessage,TResponse>(string queue, TMessage message,
        CancellationToken cancellationToken)
        where TMessage : class, IRabbitRequest where TResponse : class, IRabbitRequest;
}