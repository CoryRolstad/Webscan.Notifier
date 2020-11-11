using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace Webscan.Notifier
{
    public class NotifierService : INotifierService
    {
        private readonly SmtpClient _emailClient;
        private readonly ILogger<NotifierService> _logger;
        private readonly NotifierSettings _notifierSettings;
        
        public NotifierService(NotifierSettings notifierSettings, ILogger<NotifierService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _notifierSettings = _notifierSettings ?? throw new ArgumentNullException($"{nameof(notifierSettings)} cannot be null");            
        }

        public async Task SendHtmlEmail(string from, string to, string subject, string html)
        {
            MimeMessage email = CreateEmail(from, to, subject);
            email.Body = new TextPart(TextFormat.Html) { Text = html };
            await SendEmail(email);
        }

        public Task SendTextEmail(string from, string to, string subject, string text)
        {
            throw new NotImplementedException();
        }

        private async Task SendEmail(MimeMessage email)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(_notifierSettings.SmtpServer, _notifierSettings.Port, true);
                await smtpClient.AuthenticateAsync(_notifierSettings.UserName, _notifierSettings.Password);
                await smtpClient.SendAsync(email);
                await smtpClient.DisconnectAsync(true);
            }
        }

        private MimeMessage CreateEmail(string from, string to, string subject)
        {
            MimeMessage email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            return email; 
        }

    }
}
