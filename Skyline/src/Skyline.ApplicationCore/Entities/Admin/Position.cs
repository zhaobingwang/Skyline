using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.ApplicationCore.Entities.Admin
{
    /// <summary>
    /// 职位
    /// </summary>
    public class Position : BaseEntity<int>
    {
        /// <summary>
        /// 职位头衔
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public short Order { get; set; }
    }
}
