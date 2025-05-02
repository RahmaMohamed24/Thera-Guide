using System.Net.Mail;
using System.Net;

namespace TheraGuide.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Implement your email sending logic here
            // This example uses SMTP (configure in appsettings.json)

            var smtpServer = _configuration["Email:SmtpServer"];
            var port = int.Parse(_configuration["Email:Port"]);
            var username = _configuration["Email:Username"];
            var password = _configuration["Email:Password"];

            using (var client = new SmtpClient(smtpServer, port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(username, password);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(username),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = false
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}

