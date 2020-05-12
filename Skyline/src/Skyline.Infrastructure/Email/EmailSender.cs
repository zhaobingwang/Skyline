using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using Skyline.ApplicationCore.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private readonly EmailConfiguration _configuration;

        public EmailSender(ILogger<EmailSender> logger, EmailConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task SendAsync(EmailMessage message)
        {
            _logger.LogInformation($"[Send Email] To: {message.To}, Subject: {message.Subject}, Context: {message.Context}, From: {_configuration.FromEmail}");

            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(_configuration.FromEmail, _configuration.FromEmail));
            mail.To.Add(new MailboxAddress(message.To, message.To));
            mail.Subject = message.Subject;
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = message.Context
            };
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(_configuration.Host, _configuration.Port, true);
                client.Authenticate(_configuration.FromEmail, _configuration.FromPassword);

                await client.SendAsync(mail);
                await client.DisconnectAsync(true);
            }
        }
    }
}
