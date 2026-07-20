using MailKit.Net.Smtp;
using MimeKit;
using Qandil.Core.Interfacres.EmailService;

namespace Qandil.Infrastructure.Service.EmailService
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse("qandil.api@gmail.com"));
            email.To.Add(MailboxAddress.Parse(to));

            email.Subject = subject;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                "smtp.gmail.com",
                587,
                MailKit.Security.SecureSocketOptions.StartTls);

           
            await smtp.AuthenticateAsync(
                "qandil.api@gmail.com",
                "pdfsoiqgmpbskimg");

            await smtp.SendAsync(email);

            await smtp.DisconnectAsync(true);
        }
    }
}