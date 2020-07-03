using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.ApplicationCore.Entities.Admin
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department : BaseEntity<int>
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public short Order { get; set; }

        /// <summary>
        /// 上级部门ID
        /// </summary>
        public int ParentId { get; set; }
    }
}
