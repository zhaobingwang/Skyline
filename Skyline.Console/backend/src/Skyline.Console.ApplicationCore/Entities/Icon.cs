using Skyline.Console.ApplicationCore.Enums;
using Skyline.Console.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Entities
{
    public class Icon : BaseEntity, IAggregateRoot
    {
        public int Id { get; set; }

        /// <summary>
        /// 图标编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 图标大小(px)
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 图标颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 图标说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public IsDeleted IsDeleted { get; set; }

        public DateTime CreateTime { get; set; }

        public Guid CreateUserGuid { get; set; }
        public string CreateUserLoginName { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public Guid LastModifyUserGuid { get; set; }

        /// <summary>
        /// 最后修改用户登录名
        /// </summary>
        public string LastModifyUserLoginName { get; set; }

    }
}
