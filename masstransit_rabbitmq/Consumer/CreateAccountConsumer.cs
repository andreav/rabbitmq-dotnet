using MassTransit;
using messages;

namespace Consumer;
class CreateAccountConsumer : IConsumer<CreateAccount>
{
    public Task Consume(ConsumeContext<CreateAccount> context)
    {
        Console.WriteLine($"CreateAccount message: Name: {context.Message.Name} Email: {context.Message.Email}");
        return Task.CompletedTask;
    }
}
