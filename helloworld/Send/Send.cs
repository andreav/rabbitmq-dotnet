using System.Text;
using RabbitMQ.Client;

const string queueName = "helloworld";

Console.WriteLine("Starting Producer");

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Build queue from both producer and receiver to be sure it exists always
channel.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var cts = new CancellationTokenSource();
int messageCount = 0;
while (!cts.Token.IsCancellationRequested)
{
    messageCount++;
    string message = $"Hello World #{messageCount}";
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: string.Empty,
                        routingKey: queueName, // Same as queueName
                        basicProperties: null,
                        body: body);

    Console.WriteLine($"Sent: Message {messageCount}");
    await Task.Delay(TimeSpan.FromSeconds(1), cts.Token);
}

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
