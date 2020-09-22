using Skyline.Console.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.VO
{
    public class RoleTableVO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public bool IsSuper { get; set; }
        public bool Builtin { get; set; }
        public IsDeleted IsDeleted { get; set; }
        public DateTime LastModifyTime { get; set; }
        public string LastModifyUserLoginName { get; set; }
        public string Description { get; set; }
    }
}
