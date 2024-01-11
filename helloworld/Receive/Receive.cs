using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

const string queueName = "helloworld";

Console.WriteLine("Staring Consumer");

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Build queue from both producer and receiver to be sure it exists always
channel.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);


Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");

};

channel.BasicConsume(queue: queueName,
                    autoAck: true,
                    consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
