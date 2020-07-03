using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.ApplicationCore.Entities.Admin
{
    public class SysMenu : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public short Order { get; set; }
        public int ParentId { get; set; }
        public string[] MenuActionIds { get; set; }
    }
}
