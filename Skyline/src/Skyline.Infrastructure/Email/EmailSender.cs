using Microsoft.Extensions.Logging;
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
        public Task SendAsync(EmailMessage message)
        {
            _logger.LogInformation($"[Send Email] To: {message.To}, Subject: {message.Subject}, Context: {message.Context}, From: {_configuration.FromEmail}");
            return Task.CompletedTask;
        }
    }
}
