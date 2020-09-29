using Skyline.Console.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.VO
{
    public class PermissionListVO
    {
        /// <summary>
        /// 权限编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuGuid { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限操作码
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public IsDeleted IsDeleted { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType Type { get; set; }

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
