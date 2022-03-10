// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcServer;

//Console.WriteLine("Hello, World!");

//var input = new HelloRequest { Name = "Igriet" };
//var channel = GrpcChannel.ForAddress("https://localhost:7242/");
//var client = new Greeter.GreeterClient(channel);

//var reply = await client.SayHelloAsync(input);
//Console.WriteLine(reply.Message);


//Custom customer service
var input = new CustomerLookupModel { UserId = 1 };

var channel = GrpcChannel.ForAddress("https://localhost:7242/");
var client = new Customer.CustomerClient(channel);
var reply = await client.GetCustomerInfoAsync(input);

Console.WriteLine($"{reply.FirstName} {reply.LastName}");

Console.WriteLine();
Console.WriteLine("New Customer List");
Console.WriteLine();

using (var call = client.GetNewCustomer(new NewCustomerRequest()))
{
    while (await call.ResponseStream.MoveNext(CancellationToken.None))
    {
        var currentCustomer = call.ResponseStream.Current;
        Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName} {currentCustomer.Email} {currentCustomer.Age}, {currentCustomer.IsAlive}");
    }
}

    Console.ReadLine();