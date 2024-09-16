using System.Text;
using System.Text.Json;
using Kinetique.Shared.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Kinetique.Shared.Rpc;

public class RcpServer<TRequest,TResponse> : IDisposable where TRequest: class, IRabbitRequest where TResponse : class, IRabbitRequest
{
    private readonly EventingBasicConsumer _consumer;
    private readonly IModel _channel;
    private readonly IConnection _connection;
    
    public RcpServer(string queue,Func<TRequest,TResponse> func)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        
        _consumer = new EventingBasicConsumer(_channel);

        _consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var props = ea.BasicProperties;
            var replyProps = _channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            TRequest requestMsg = default;
            try
            {
                requestMsg = JsonSerializer.Deserialize<TRequest>(body);
            }
            catch (Exception e)
            {
                Console.WriteLine($" [.] {e.Message}");
            }

            var response = func.Invoke(requestMsg);
            var serializedResponse = JsonSerializer.SerializeToUtf8Bytes(response);
            _channel.BasicPublish(exchange: string.Empty,
                routingKey: props.ReplyTo,
                basicProperties: replyProps,
                body: serializedResponse);
            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };
        
        _channel.BasicConsume(queue: queue,
            autoAck: false,
            consumer: _consumer);
    }

    public void Dispose()
    {
        if (_connection is { IsOpen: true })
        {
            _connection.Dispose();
            _channel.Dispose();
        }
    }
}