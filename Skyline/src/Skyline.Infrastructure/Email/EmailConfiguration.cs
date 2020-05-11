using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Infrastructure.Email
{
    public class EmailConfiguration
    {
        public string FromEmail { get; }
        public EmailConfiguration(string fromEmail)
        {
            FromEmail = fromEmail;
        }
    }
}
