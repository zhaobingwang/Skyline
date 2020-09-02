using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.BO
{
    public class UserBO
    {
        public Guid Id { get; set; }
        public string LoginName { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public DateTime? DOB { get; set; }
    }
}
