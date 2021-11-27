using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace RabbitMq.publisher
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://eiatkmzn:27lBAUCikEgrFkr5moCuxjDuT_NCpxqf@fish.rmq.cloudamqp.com/eiatkmzn");
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("hello-queue", true,false,false);
            Enumerable.Range(1, 20).ToList().ForEach(x =>
            {
                string message = $"Message {x}";
                var messageBody = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);
                Console.WriteLine($"Mesajınız Gönderildi: {message}");
            });
            Console.ReadLine();
        }
    }
}
