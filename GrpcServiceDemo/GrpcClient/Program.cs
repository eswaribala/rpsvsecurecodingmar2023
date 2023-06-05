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