using Skyline.Console.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.VO
{
    public class EditUserVO
    {
        public Guid Id { get; set; }
        public string LoginName { get; set; }
        public string NickName { get; set; }
        public DateTime? DOB { get; set; }
        public UserType Type { get; set; }
        public string Password { get; set; }
    }
}
