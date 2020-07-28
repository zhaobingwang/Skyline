using Skyline.Console.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Console.ApplicationCore.Entities
{
    public class SysRole
    {
        public SysRole()
        {
            UserRoles = new HashSet<SysUserRole>();
            RolePermissions = new HashSet<SysRolePermission>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public IsDeleted IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? ModifyTime { get; set; }
        public Guid ModifiyUserId { get; set; }
        public string ModifyUserName { get; set; }
        public bool IsSuperAdministrator { get; set; }

        /// <summary>
        /// 是否是系统内置角色(系统内置角色不允许删除,修改操作)
        /// </summary>
        public bool Builtin { get; set; }

        public ICollection<SysUserRole> UserRoles { get; set; }
        public ICollection<SysRolePermission> RolePermissions { get; set; }
    }
}
