using System.Text;
using System.Text.Json;
using Kinetique.Shared.Rpc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Kinetique.Shared.Messaging;

internal sealed class RabbitPublisher(IConnection connection) : IRabbitPublisher
{
    public void PublishToQueue<TMessage>(TMessage message, string exchange) where TMessage : class, IRabbitRequest
    {
        var json = JsonSerializer.Serialize(message);
        var messageBytes = Encoding.UTF8.GetBytes(json);
        using var channel = connection.CreateModel();
        channel.BasicPublish(exchange: exchange,
            routingKey: string.Empty,
            basicProperties: null,
            body: messageBytes);
    }

    public void PublishToExchange<TMessage>(TMessage message, string exchange, string routingKey = default) where TMessage : class, IRabbitRequest
    {
        var json = JsonSerializer.Serialize(message);
        var messageBytes = Encoding.UTF8.GetBytes(json);
        using var channel = connection.CreateModel();
        channel.BasicPublish(exchange: exchange,
            routingKey: routingKey,
            basicProperties: null,
            body: messageBytes);
    }

    public async Task<TResponse> PublishRequest<TMessage, TResponse>(string queue, TMessage message, CancellationToken cancellationToken) where TMessage : class, IRabbitRequest where TResponse : class, IRabbitRequest
    {
        var rpcClient = new RpcClient<TMessage,TResponse>(queue);
        var res = await rpcClient.CallAsync(message);
        return res;
    }
}