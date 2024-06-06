using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace rabbitmq_webapi.Messaging;

public class RabbitMQProducer : IRabbitMQProducer
{
    private readonly IConfiguration _configuration;

    public RabbitMQProducer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendProductMessage<T>(string exchangeName, string routingKey, string queueName, T message)
    {

        var factory = new ConnectionFactory
        {
            Uri = new(_configuration.GetConnectionString("RabbitMQ")),
        };

        var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
        channel.QueueDeclare(queueName, exclusive: false, durable: false);
        channel.QueueBind(queueName, exchangeName, routingKey);
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, body: body);

        channel.Close();
        connection.Close();
    }

}
