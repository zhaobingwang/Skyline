using Skyline.Console.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.BO
{
    public class MenuTableBO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public Guid ParentId { get; set; }
        public string ParentName { get; set; }
        public Status Status { get; set; }
        public IsDeleted IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserLoginName { get; set; }
        public DateTime LastModifyTime { get; set; }
        public string LastModifyUserLoginName { get; set; }
    }
}
