# Setup Helloworld

    mkdir helloworld
    cd helloworld
    
    dotnet new console --name Send
    mv Send/Program.cs Send/Send.cs

    dotnet new console --name Receive
    mv Receive/Program.cs Receive/Receive.cs

    cd Send
    dotnet add package RabbitMQ.Client
    cd ../Receive
    dotnet add package RabbitMQ.Client


# Setup MassTransit

    mkdir masstransit_rabbitmq
    cd masstransit_rabbitmq

    dotnet new classlib -n messages -o Messages
    mv .\Messages\Class1.cs .\Messages\Message.cs

    dotnet new console --name Producer
    mv Producer/Program.cs Producer/Producer.cs
    dotnet add .\Producer\Producer.csproj reference .\Messages\messages.csproj

    dotnet new console --name Consumer
    mv Consumer/Program.cs Consumer/Consumer.cs
    dotnet add .\Consumer/Consumer.csproj reference .\Messages\messages.csproj

    cd ..
    dotnet sln add .\masstransit_rabbitmq\Producer\Producer.csproj
    dotnet sln add .\masstransit_rabbitmq\Consumer\Consumer.csproj
    dotnet sln add .\masstransit_rabbitmq\Messages\messages.csproj

    cd .\masstransit_rabbitmq\Producer\
    dotnet add package MassTransit
    dotnet add package  MassTransit.RabbitMQ

    cd ..\Consumer
    dotnet add package MassTransit
    dotnet add package  MassTransit.RabbitMQ


With RabbitMQ, which supports exchanges and queues, messages are sent or published to exchanges and RabbitMQ routes those messages through exchanges to the appropriate queues.

When the bus is started, MassTransit will create exchanges and queues on the virtual host for the receive endpoint. 

MassTransit creates **durable**, **fanout** exchanges by default, and queues are also **durable** by default.


Note:

* First start consumer who creates:
    * Exchange messages:<messageTypename> - durable + fanout
    * Queue:<messageTypename> - durable

