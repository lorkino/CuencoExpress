using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Film.Models
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("diluma95@gmail.com", "lorkino95"),
                EnableSsl = true
                
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress("diluma95@gmail.com", "ServiceExpress")
            };
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = htmlMessage;
            return client.SendMailAsync(mailMessage);
        }
    }
}