using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Console.ApplicationCore.Entities
{
    public class RolePermissionMapping
    {
        public string RoleCode { get; set; }
        public string PermissionCode { get; set; }
        public Role Role { get; set; }
        public Permission Permission { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
