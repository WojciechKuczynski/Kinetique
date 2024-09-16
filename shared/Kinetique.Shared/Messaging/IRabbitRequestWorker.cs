namespace Kinetique.Shared.Messaging;

public interface IRabbitRequestWorker
{
    Task OnMessageReceived<TMessage,TResponse>(string queue, Func<TMessage, TResponse> handle,
        CancellationToken cancellationToken)
        where TMessage : class, IRabbitRequest where TResponse : class, IRabbitRequest;
}