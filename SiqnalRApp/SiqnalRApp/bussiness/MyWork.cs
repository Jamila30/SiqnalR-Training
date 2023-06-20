using Microsoft.AspNetCore.SignalR;
using SiqnalRApp.Hubs;

namespace SiqnalRApp.bussiness
{
	public class MyWork 
	{
		readonly IHubContext<MyHub> _hubContext;

		public MyWork(IHubContext<MyHub> hubContext)
		{
			_hubContext = hubContext;
		}

		public async void SendMessageAsync(string message)
		{

			await _hubContext.Clients.All.SendAsync("recieveMessage", message);

		}
	}
}
