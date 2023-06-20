namespace SiqnalRApp.Models
{
	public class Group
	{
		public string? Groupname { get; set; }
		public  List<Client> Clients { get; }=new List<Client>();
	}
}
