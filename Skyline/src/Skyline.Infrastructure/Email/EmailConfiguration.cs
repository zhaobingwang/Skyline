using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Infrastructure.Email
{
    public class EmailConfiguration
    {
        public string FromEmail { get; }
        public string FromPassword { get; }
        public string Host { get; set; }
        public int Port { get; set; }
        public EmailConfiguration(string fromEmail, string fromPassword, string host, int port)
        {
            FromEmail = fromEmail;
            FromPassword = fromPassword;
            Host = host;
            Port = port;
        }
    }
}
