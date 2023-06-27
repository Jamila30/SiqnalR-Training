using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using SiqnalRApp.Models;

namespace SiqnalRApp.Hubs
{
	public class RabbitMessageHub: Hub
	{
		public async Task SendMessageAsync(string message)
		{
			await Clients.All.SendAsync("recieveMessage",message);
		}
	}
}
