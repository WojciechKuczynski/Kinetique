namespace Kinetique.Shared.Messaging;

public interface IRabbitConsumer
{
    Task OnMessageReceived<TMessage>(string queue, Action<TMessage> handle, CancellationToken cancellationToken) 
        where TMessage : class, IRabbitRequest;
}