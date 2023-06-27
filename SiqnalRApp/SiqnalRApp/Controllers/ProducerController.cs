using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using SiqnalRApp.Models;
using System.Text;
using System.Text.Json;

namespace SiqnalRApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProducerController : ControllerBase
	{
		[HttpPost]
		public IActionResult SendToProducer([FromForm]User user)
		{
			ConnectionFactory  factory= new ConnectionFactory();
			factory.Uri=new Uri("amqps://iaeugyqy:FYEIq9qgRNTCduaNi08tu2Z97u744YpD@goose.rmq2.cloudamqp.com/iaeugyqy");
			using IConnection connection= factory.CreateConnection();
			using IModel channel=connection.CreateModel();

			channel.QueueDeclare("rabbitQueue", false, false, false);
			string dataModel=JsonSerializer.Serialize(user);
			byte[] data=Encoding.UTF8.GetBytes(dataModel);
			channel.BasicPublish(exchange: "", "rabbitQueue", body: data);

			return Ok();
		}
	}
}
