using ContextBuider.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using MQTTnet.Client;
using ContextBuider.Services;
using ContextBuider.Models;

namespace ContextBuider
{
    class Program
    {

        static void Main(string[] args)
        {
            using var _contex = new ContextAwareDb();
            var _logic = new Logic();



            var factory = new ConnectionFactory { HostName = "192.168.28.86" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "ContinentalExchange", type: ExchangeType.Topic, durable: true);
            // declare a server-named queue
            var queueName = channel.QueueDeclare("", exclusive: true).QueueName;
            channel.QueueBind(exchange: "ContinentalExchange", queue: queueName, routingKey: "#.#");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;
                Console.WriteLine($" [x] Received '{routingKey}':'{message}'");
                _logic.readMessage(routingKey, message, _contex);
          
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();


        }
       
    }

}