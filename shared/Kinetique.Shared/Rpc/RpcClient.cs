using System.Collections.Concurrent;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Kinetique.Shared.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Kinetique.Shared.Rpc;

public class RpcClient<TRequest,TResponse> : IDisposable where TRequest: class, IRabbitRequest where TResponse: class, IRabbitRequest
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<TResponse>> _callbackMapper = new();
    private IBasicProperties props;
    private string _rpcQueueName;
    private string _rpcQueueNameReply; 

    public RpcClient(string connectionString)
    {
        var factory = new ConnectionFactory() { Uri = new Uri(connectionString) };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void Configure(string queueName)
    {
        _rpcQueueName = queueName;

        _rpcQueueNameReply = _channel.QueueDeclare().QueueName;

        var consumer = new EventingBasicConsumer(_channel);
        props = _channel.CreateBasicProperties();
        props.Persistent = true;
        var correlationId = Guid.NewGuid().ToString();
        props.CorrelationId = correlationId;
        props.ReplyTo = _rpcQueueNameReply;
        
        consumer.Received += (model, ea) =>
        {
            if (!_callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs)) return;

            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            var responseMessage = JsonSerializer.Deserialize<TResponse>(response);
            if (ea.BasicProperties.CorrelationId == correlationId)
            {
                tcs.TrySetResult(responseMessage);
            }
        };

        _channel.BasicConsume(consumer: consumer, queue: _rpcQueueNameReply, autoAck: true);
    }
    
    
    public Task<TResponse> CallAsync(TRequest message, CancellationToken token = default)
    {
        var json = JsonSerializer.Serialize(message);
        var messageBytes = Encoding.UTF8.GetBytes(json);
        
        _channel.BasicPublish(exchange: string.Empty,
                              routingKey: _rpcQueueName,
                              basicProperties: props,
                              body: messageBytes);
        var tcs = new TaskCompletionSource<TResponse>();
        _callbackMapper.TryAdd(props.CorrelationId, tcs);
        token.Register(() =>
        {
            _callbackMapper.TryRemove(props.CorrelationId, out _);
        });
        
        return tcs.Task;
    }
    
    public void Dispose()
    {
        _connection.Close();
    }
}