using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using rabbitmq_receiver.DataAccess;
using rabbitmq_receiver.Messaging;
using rabbitmq_receiver.Repositories;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=postgresdb;User Id=postgres;Password=password;");
});

using IHost host = builder.Build();

Run(host.Services.GetRequiredService<IRabbitMQProducer>());

await host.RunAsync();
static void Run(IRabbitMQProducer rabbitMQProducer)
{
    string exchangeName = "UserExchange";
    string routingKey = "UserRoutingKey";
    string queueName = "UserQueue";
    rabbitMQProducer.ReceiveProductMessage(exchangeName, routingKey, queueName);
}