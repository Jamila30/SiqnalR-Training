using Microsoft.AspNetCore.SignalR;

namespace SiqnalRApp.Hubs
{
	public class MyHub: Hub
	{
		public async void SendMessageAsync(string message)
		{

			await Clients.All.SendAsync("recieveMessage", message);

		}
	}
}
