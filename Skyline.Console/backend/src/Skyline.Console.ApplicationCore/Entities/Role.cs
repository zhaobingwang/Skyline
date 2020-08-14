using Skyline.Console.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Console.ApplicationCore.Entities
{
    public class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRoleMapping>();
            RolePermissions = new HashSet<RolePermissionMapping>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public IsDeleted IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid CreateUserGuidId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? ModifyTime { get; set; }
        public Guid ModifiyUserId { get; set; }
        public string ModifyUserName { get; set; }
        public bool IsSuperAdministrator { get; set; }

        /// <summary>
        /// 是否是系统内置角色(系统内置角色不允许删除,修改操作)
        /// </summary>
        public bool Builtin { get; set; }

        public ICollection<UserRoleMapping> UserRoles { get; set; }
        public ICollection<RolePermissionMapping> RolePermissions { get; set; }
    }
}
