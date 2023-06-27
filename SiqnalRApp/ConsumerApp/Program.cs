using ConsumerApp;
using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SiqnalRApp.Models;
using System.Text;
using System.Text.Json;


//siqnalR
HubConnection siqnalRConnection = new HubConnectionBuilder().WithUrl("https://localhost:7046/rabbitHub").WithAutomaticReconnect().Build();
await siqnalRConnection.StartAsync();


ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("CloudAMQP url here");
IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare("rabbitQueue", false, false, false);
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume("rabbitQueue", true, consumer);
consumer.Received +=  async (model, eventArgs) =>
{
	string userJson = Encoding.UTF8.GetString(eventArgs.Body.Span);
	User user =JsonSerializer.Deserialize<User>(userJson);

	EmailSender.SendEmail(user.Email, user.Message);
	await siqnalRConnection.InvokeAsync("SendMessageAsync", $"{user.Email} sent messages");
	Console.WriteLine("aaalmaaaa");
};
Console.Read();
