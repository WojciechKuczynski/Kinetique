using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Kinetique.Shared.Messaging;

public class RabbitRequestWorker(IConnection connection)  : IRabbitRequestWorker
{
    public async Task OnMessageReceived<TMessage, TResponse>(string queue, Func<TMessage, TResponse> handle, CancellationToken cancellationToken) where TMessage : class, IRabbitRequest where TResponse : class, IRabbitRequest
    {
        var taskCompletionSource = new TaskCompletionSource();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        
        var consumer = new EventingBasicConsumer(channel);
        channel.BasicConsume(queue: queue,
            autoAck: false,
            consumer: consumer);
        Console.WriteLine(" [x] Awaiting RPC requests");

        consumer.Received += (model, ea) =>
        {
            TResponse response = default;

            var body = ea.Body.ToArray();
            var props = ea.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(body);
                var m = JsonSerializer.Deserialize<TMessage>(message);
                response = m as TResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine($" [.] {e.Message}");
            }
            finally
            {

                var serialized = JsonSerializer.Serialize(response);
                var responseBytes = Encoding.UTF8.GetBytes(serialized);
                channel.BasicPublish(exchange: string.Empty,
                    routingKey: props.ReplyTo,
                    basicProperties: replyProps,
                    body: responseBytes);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
        };
        
        cancellationToken.Register(() => taskCompletionSource.SetCanceled(cancellationToken));
        await taskCompletionSource.Task;
    }
}