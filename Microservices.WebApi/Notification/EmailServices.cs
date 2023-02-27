using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
namespace Notification
{
    public class EmailServices : IEmailServices
    {
        public Task sendEmail(NotificationMessage message)
        {
            var from = new MailAddress("from@example.com");
            var to = new MailAddress("monzermohamed02@gamil.com");
            var subject = "Email with attachment";
            var body = $"<h1>{message.Message}</h1>" ; 
            //var username = "username"; // get from Mailtrap
            //var password = "password"; // get from Mailtrap 
            var host = "92.205.18.114"; 
            var port = 8025; 

            var client = new SmtpClient(host, port);
            //   client.Credentials = new NetworkCredential(username, password);
            client.EnableSsl = true;

            var mail = new MailMessage();
            mail.Subject = subject;
            mail.From = from;
            mail.To.Add(to);
            mail.Body = body; 
            client.Send(mail);
            return Task.CompletedTask;
        }
    }
}
