# Setup

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