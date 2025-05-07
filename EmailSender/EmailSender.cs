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

            var smtpServer = _configuration["EmailConfiguration:SmtpServer"];
            var port = int.Parse(_configuration["EmailConfiguration:Port"]);
            var username = _configuration["EmailConfiguration:Username"];
            var password = _configuration["EmailConfiguration:Password"];

            using (var client = new SmtpClient(smtpServer, port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(username, password);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(username),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true

                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}

