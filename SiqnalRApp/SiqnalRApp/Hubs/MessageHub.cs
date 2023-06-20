using Microsoft.AspNetCore.SignalR;
using System.Xml.Linq;

namespace SiqnalRApp.Hubs
{
	public class MessageHub : Hub
	{
		//public async void SendMessage(string message)
		//{
		//	//await Clients.All.SendAsync("recieveMessage", message);//hamiya
		//	//await Clients.Caller.SendAsync("recieveMessage", message); // sadece ozune
		//	await Clients.Others.SendAsync("recieveMessage",message);// ozunden basqa hamiya
		//}

		//public async void SendMessage(string message, IEnumerable<string> connectionIds)
		//{
		//	//await Clients.AllExcept(connectionIds).SendAsync("recieveMessage",message); //istisna olmaqla deyer gonderir
		//	//await Clients.Clients(connectionIds).SendAsync("recieveMessage", message);//sirf secdiyimiz clientlere gonderir
		//	//await Clients.Client(connectionIds.FirstOrDefault()).SendAsync("recieveMessage", message);
		//}

		//public async void AddToGroup(string connectionId, string groupname)
		//{
		//	await Groups.AddToGroupAsync(connectionId, groupname);
		//}

		//public async void SendMessage(IEnumerable<string> connectionIDs , string groupname , string message)
		//{
		//	await Clients.GroupExcept(groupname, connectionIDs).SendAsync("recieveMessage", message);
		//}

		//public async void SendMessage(string message, string groupname)
		//{
		//	await Clients.Group(groupname).SendAsync("recieveMessage", message);//o qrupdaki hamiya
		//	await Clients.OthersInGroup(groupname).SendAsync("recieveMessage", message);//o qrupdaki caller xaric hamiya
		//}

		//public async void SendMessage(string message)
		//{
		//	await Clients.Groups("groupA", "groupB").SendAsync("recieveMessage", message);
		//}
		public override async Task OnConnectedAsync()
		{

			await Clients.Caller.SendAsync("getIds", Context.ConnectionId);
		}
	}
}
