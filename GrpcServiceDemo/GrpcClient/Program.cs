// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcServiceDemo;

Console.WriteLine("Hello, World!");
//request template

var data = new HelloRequest { Name = "Parameswari" };
//access the channel 
var grpcChannel = GrpcChannel.ForAddress("https://localhost:7283");
//delete grpc service in .csproj build and then add
var client = new Greeter.GreeterClient(grpcChannel);
var response = await client.SayHelloAsync(data);
Console.WriteLine(response.Message);

var customerData = new AddRequest
{
    Customer = new Customer
    {
        CustomerId = 32858,
        FirstName = "Parameswari",
        LastName = "Bala",
        Email = "Parameswaribala@gmail.com"
    }
};

var grpcCustomerChannel = GrpcChannel.ForAddress("https://localhost:7283");

var customerClient = new CustomerProcess.CustomerProcessClient(grpcCustomerChannel);

var customerResponse = await customerClient.AddCustomerAsync(customerData);
Console.WriteLine(customerResponse.Message);

Console.ReadKey();