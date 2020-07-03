using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Skyline.ApplicationCore.Entities.Admin
{
    /// <summary>
    /// 用户
    /// </summary>
    public class SysUser : BaseEntity<Guid>
    {
        public string NickName { get; set; }
        public string RealName { get; set; }
        public string DepartmentId { get; set; }
        public string[] PositionIds { get; set; }
        public string[] RoleIds { get; set; }
        public string CellPhoneNumber { get; set; }
        public string PasswordHash { get; set; }

        public AccountStatus Status { get; set; }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSuperAdministrator { get; set; }
    }
    public enum AccountStatus
    {
        [Display(Name = "未激活")]
        NonActivated = 0,

        [Display(Name = "正常")]
        Normal = 1,

        [Display(Name = "已删除")]
        Deleted = 2,

        [Display(Name = "已停用")]
        Disabled = 3,
    }
}
