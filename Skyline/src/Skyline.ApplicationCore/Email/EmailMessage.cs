using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.ApplicationCore.Email
{
    public class EmailMessage
    {
        public string To { get; }
        public string Subject { get; }
        public string Context { get; }
        public EmailMessage(string to, string subject, string content)
        {
            To = to;
            Subject = subject;
            Context = content;
        }
    }
}
