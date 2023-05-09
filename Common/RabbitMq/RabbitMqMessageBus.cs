using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Common.RabbitMq
{
    /// <summary>
    /// Message bus class
    /// </summary>
    public class RabbitMqMessageBus : IMessageBus, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqMessageBus(IConnection connection)
        {
            _connection = connection;
            _channel = _connection.CreateModel();
        }

        public void Publish<T>(T message, string exchangeName, string routingKey)
        {
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: exchangeName,
                                  routingKey: routingKey,
                                  basicProperties: null,
                                  body: body);
        }

        public void Subscribe<T>(string exchangeName, string queueName, string routingKey, Action<T> handler)
        {
            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);

            _channel.QueueDeclare(queue: queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            _channel.QueueBind(queue: queueName,
                               exchange: exchangeName,
                               routingKey: routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var message = JsonConvert.DeserializeObject<T>(json);
                handler(message);
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }


        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
