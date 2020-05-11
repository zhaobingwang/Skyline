using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.ApplicationCore.Email
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message);
    }
}
