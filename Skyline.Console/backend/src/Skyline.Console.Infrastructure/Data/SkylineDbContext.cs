using Microsoft.EntityFrameworkCore;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Skyline.Console.Infrastructure.Data
{
    public class SkylineDbContext : DbContext
    {
        public SkylineDbContext(DbContextOptions<SkylineDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public DbSet<Menu> Menus { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public DbSet<Icon> Icons { get; set; }

        /// <summary>
        /// 用户-角色 多对多映射
        /// </summary>
        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }

        /// <summary>
        /// 角色权限 多对多映射
        /// </summary>
        public DbSet<RolePermissionMapping> RolePermissionMappings { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
