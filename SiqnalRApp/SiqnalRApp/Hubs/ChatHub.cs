using Microsoft.AspNetCore.SignalR;
using SiqnalRApp.Datas;
using SiqnalRApp.Models;

namespace SiqnalRApp.Hubs
{
	public class ChatHub : Hub
	{
		public async Task GetNickname(string nickname)
		{
			Client client = new Client()
			{
				ConnectionId = Context.ConnectionId,
				Nickname = nickname
			};

			ClientStorage.Clients.Add(client);
			await Clients.Others.SendAsync("ClientJoined", nickname);
			await Clients.All.SendAsync("clients", ClientStorage.Clients);
			await Clients.All.SendAsync("groups", GroupStorage.Groups);
		}
		public async Task SendMessage(string message, string clientName)
		{
			Client? senderClient = ClientStorage.Clients.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
			string username = clientName.Trim();
			if (clientName == "Hami")
			{
				await Clients.Others.SendAsync("getMessage", message, senderClient?.Nickname);
			}
			else
			{
				Client? client = ClientStorage.Clients.FirstOrDefault(c => c.Nickname == username);
				await Clients.Client(client!.ConnectionId).SendAsync("getMessage", message, senderClient?.Nickname);
			}
		}

		public async Task AddGroup(string groupName)
		{

			Client? client = ClientStorage.Clients.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
			await Groups.AddToGroupAsync(client!.ConnectionId, groupName);
			Group group = new Group { Groupname = groupName };
			GroupStorage.Groups.Add(group);
			group?.Clients.Add(client);

			await Clients.All.SendAsync("group", groupName);
			await Clients.All.SendAsync("groups", GroupStorage.Groups);
		}

		public async Task AddClientToGroup(IEnumerable<string> groupNames)
		{
			Client? client = ClientStorage.Clients.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
			foreach (var groupName in groupNames)
			{
				await Groups.AddToGroupAsync(client!.ConnectionId, groupName);
				Group? group = GroupStorage.Groups.FirstOrDefault(g => g.Groupname == groupName);
				group?.Clients.Add(client);
			}
		}

		public async Task GetGroupClients(string groupName)
		{
			Group? group = GroupStorage.Groups.FirstOrDefault(g => g.Groupname == groupName);
			await Clients.Caller.SendAsync("getUsersInGroup", group?.Clients);
		}


	}
}
