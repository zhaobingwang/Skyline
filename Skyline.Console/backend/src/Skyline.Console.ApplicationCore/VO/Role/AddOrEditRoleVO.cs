using Skyline.Console.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.VO
{
    public class AddOrEditRoleVO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public string Description { get; set; }
    }
}
