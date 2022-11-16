using CMS.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Net.Mail;

namespace CMS.Services
{
    public class EmailService : IEmailService
    {
        EmailSettings _emailSettings = null;
        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendEmail(EmailData emailData)
        {
            string fromMail = _emailSettings.EmailId;
            string fromPassword = _emailSettings.Password;
            string toMail = emailData.EmailToId;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = emailData.EmailSubject;
            message.To.Add(new MailAddress(toMail));
            message.Body = emailData.EmailBody;
            message.IsBodyHtml = true;

            var smtpClient = new System.Net.Mail.SmtpClient(_emailSettings.Host)
            {
                Port = _emailSettings.Port,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = _emailSettings.UseSSL,
            };

            smtpClient.Send(message);
        }
    }
}
