using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Tools.SeedData.Data.Entities
{
    public class Tmp
    {
        public int id { get; set; }
        public DateTime time { get; set; }
        public DateTime time_utc { get; set; }
        public DateTimeOffset time_offset { get; set; }
        public string remark { get; set; }
        public bool is_delete { get; set; }
    }
}
