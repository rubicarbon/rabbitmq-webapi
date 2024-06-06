namespace rabbitmq_receiver.Messaging;

public interface IRabbitMQProducer
{
    void ReceiveProductMessage(string exchangeName, string routingKey, string queueName);
}