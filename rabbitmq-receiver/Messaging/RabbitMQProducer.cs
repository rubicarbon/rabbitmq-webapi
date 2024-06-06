using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using rabbitmq_receiver.Models;
using rabbitmq_receiver.Repositories;
using System.Text;
using System.Text.Json;

namespace rabbitmq_receiver.Messaging;

public class RabbitMQProducer : IRabbitMQProducer
{
    private readonly IUserRepository _userRepository;

    public RabbitMQProducer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void ReceiveProductMessage(string exchangeName, string routingKey, string queueName)
    {

        var factory = new ConnectionFactory
        {
            Uri = new("amqp://guest:guest@localhost:5672/"),
        };

        var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
        channel.QueueDeclare(queueName, exclusive: false, durable: false);
        channel.QueueBind(queueName, exchangeName, routingKey);
        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            User user = JsonSerializer.Deserialize<User>(message);
            _userRepository.AddUser(user);
            Console.WriteLine($" [x] Received {message}");
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };
        string consumerTag = channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

        Console.ReadLine();

        channel.Close();
        connection.Close();
    }

}
