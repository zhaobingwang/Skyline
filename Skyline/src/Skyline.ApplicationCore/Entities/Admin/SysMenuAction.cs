using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.ApplicationCore.Entities.Admin
{
    public class SysMenuAction : BaseEntity<int>
    {
        public string Name { get; set; }

        public List<string> Urls { get; set; }
    }
}
