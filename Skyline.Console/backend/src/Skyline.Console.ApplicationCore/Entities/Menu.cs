using Skyline.Console.ApplicationCore.Enums;
using Skyline.Console.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Entities
{
    public class Menu : BaseEntity, IAggregateRoot
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Alias { get; set; }
        public string Icon { get; set; }
        public Guid ParentGuid { get; set; }
        public string ParentName { get; set; }

        /// <summary>
        /// 菜单层级深度
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public IsDeleted IsDeleted { get; set; }

        /// <summary>
        /// 是否为默认路由
        /// </summary>
        public YesOrNo IsDefaultRouter { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        public Guid CreateUserGuid { get; set; }

        /// <summary>
        /// 创建者登录名
        /// </summary>
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

        /// <summary>
        /// 是否隐藏菜单
        /// </summary>
        public YesOrNo? HideMenu { get; set; }

        /// <summary>
        /// 菜单拥有的权限集合
        /// </summary>
        public ICollection<Permission> Permissions { get; set; }
    }
}
