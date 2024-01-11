using MassTransit;
using messages;

Console.WriteLine("Start Producer");

var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host(new Uri("rabbitmq://localhost"), h =>
    {
        h.Username("guest");
        h.Password("guest");
    });
});

bus.Start();

int messageCount = 0;
var cts = new CancellationTokenSource();

Console.WriteLine("Sending messages. Press any key to stop...");

while (!cts.Token.IsCancellationRequested)
{
    messageCount++;

    await bus.Publish(new CreateAccount
    {
        Name = $"iteration {messageCount}",
        Email = $"iteration-{messageCount}@mail.com"
    }, cts.Token);

    Console.WriteLine($"Sent: Message {messageCount}");
    await Task.Delay(TimeSpan.FromSeconds(1), cts.Token);
}

bus.Stop();
