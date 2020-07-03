using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Skyline.ApplicationCore.Entities.Admin
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTimeUTC { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTimeUTC { get; set; }
    }
}
