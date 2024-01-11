using messages;
using MassTransit;
using Consumer;

Console.WriteLine("Starting Consumer");

var bus = Bus.Factory.CreateUsingRabbitMq(rabbit =>
  {
      rabbit.Host(new Uri("rabbitmq://localhost"), settings =>
      {
          settings.Username("guest");
          settings.Password("guest");
      });

      // Questa queue è il default creato da MassTransit
      rabbit.ReceiveEndpoint("messages:CreateAccount", ep =>
      {
          // Se accodo N messaggi e lancio il Consumer, vengono scodati in ordine sparso.
          // Se forzo size = 1 => arrivano in ordine
          //
          // ep.PrefetchCount = 1;

          ep.Consumer<CreateAccountConsumer>();
          // In alternativa - soluzione inline
          //
          //   ep.Handler<CreateAccount>(context =>
          //   {
          //       return Console.Out.WriteLineAsync($"[CREATEACCOUNT {DateTime.Now.ToString("s")}] Name: {context.Message.Name}, Email: {context.Message.Email}");
          //   });
      });
  });

bus.Start();

// let the bus running and quits when user press Enter on the terminal
Console.Write("Press any key to quit...");
Console.ReadLine();

// stops the bus and ends the connection to RabbitMQ
bus.Stop();
