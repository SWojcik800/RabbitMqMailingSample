namespace Common.RabbitMq
{
    /// <summary>
    /// Interface for message bus for RabbitMq
    /// </summary>
    public interface IMessageBus
    {
        void Publish<T>(T message, string exchangeName, string routingKey);
        void Subscribe<T>(string exchangeName, string queueName, string routingKey, Action<T> handler);
    }
}