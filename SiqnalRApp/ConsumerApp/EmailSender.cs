using System.Net;
using System.Net.Mail;

namespace ConsumerApp
{
	public static class EmailSender
	{
		public static async void SendEmail(string toEmail, string message)
		{
			try
			{
				// use the Gmail SMTP Host
				SmtpClient client = new SmtpClient("smtp.gmail.com");
				// enable SSL for encryption across channels
				client.EnableSsl = true;
				// Port 465 for SSL communication
				client.Port = 587;

				// Provide authentication information with Gmail SMTP server to authenticate your sender account
				NetworkCredential credential=new NetworkCredential("testtestovv.2023@gmail.com", "ntdmfhbsaknytxhf");
				client.Credentials = credential;


				MailAddress alici = new MailAddress(toEmail);
				MailAddress gonderen = new MailAddress("testtestovv.2023@gmail.com", "Jamila Ismayilzada");

				MailMessage newMail = new MailMessage(gonderen,alici);
				newMail.Subject = "SiqnalR example";
				newMail.Body = message;
				await client.SendMailAsync(newMail); // Send the constructed mail
				Console.WriteLine("Email Sent");

			}
			catch (Exception ex)
			{
				Console.WriteLine("Error -" + ex);
			}
		}
	}
}
