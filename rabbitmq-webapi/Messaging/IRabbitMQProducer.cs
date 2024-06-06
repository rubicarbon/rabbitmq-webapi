namespace rabbitmq_webapi.Messaging;

public interface IRabbitMQProducer
{
    void SendProductMessage<T>(string exchangeName, string routingKey, string queueName, T message);
}