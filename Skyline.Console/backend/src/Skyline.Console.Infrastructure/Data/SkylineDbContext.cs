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

        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysUserRole> SysUserRoles { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysRolePermission> SysRolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
