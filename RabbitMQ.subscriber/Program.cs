using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQ.subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://eiatkmzn:27lBAUCikEgrFkr5moCuxjDuT_NCpxqf@fish.rmq.cloudamqp.com/eiatkmzn");
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            //channel.QueueDeclare("hello-queue", true, false, false);  publisher da hello-queue adında kuyruk olusturduysak yazmamıza gerek 
            // olusturmamış ı-şse yazıp olusturmalıyız.
            channel.BasicQos(0,4,true);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("GTECH2",false,consumer);
            consumer.Received += (object sender, BasicDeliverEventArgs e)   =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Thread.Sleep(1500);
                Console.WriteLine("Gelen Mesaj : " + message);
                channel.BasicAck(e.DeliveryTag, false);
            };
            Console.ReadLine();
        }        
    }
}
