using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.ApplicationCore.Entities.Admin
{
    /// <summary>
    /// 角色
    /// </summary>
    public class SysRole : BaseEntity<int>
    {
        public string Name { get; set; }
        public string[] MenuIds { get; set; }
        public string[] MenuActionIds { get; set; }
    }
}
