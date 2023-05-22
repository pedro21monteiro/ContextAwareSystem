using RabbitMQ.Client;
using System.Text;

namespace ContinentalTestDb.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        //private IModel _channel;
        //private readonly string rabbitHost = System.Environment.GetEnvironmentVariable("RABBITHOST") ?? "192.168.28.86";
        public RabbitMqService()
        {
            //_channel = new ConnectionFactory() { HostName = rabbitHost }.CreateConnection().CreateModel();
            //_channel.ExchangeDeclare(exchange: "ContinentalExchange", type: "topic", durable: true);
        }

        //public async Task PublishMessage(string message, string topic)
        //{

        //    var body = Encoding.UTF8.GetBytes(message);
        //    _channel.BasicPublish(exchange: "ContinentalExchange",
        //                         routingKey: topic,
        //                         basicProperties: null,
        //                         body: body);
        //    Console.WriteLine(" [x] Sent '{0}':'{1}'", topic, message);

        //}
      

    }
}
