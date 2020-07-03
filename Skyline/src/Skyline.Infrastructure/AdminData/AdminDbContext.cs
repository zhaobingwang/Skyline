using Microsoft.EntityFrameworkCore;
using Skyline.ApplicationCore.Entities.Admin;
using Skyline.Infrastructure.AdminData.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Skyline.Infrastructure.AdminData
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
        {

        }
        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysMenu> SysMenus { get; set; }
        public DbSet<SysMenuAction> SysMenuActions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new SysUserConfiguration());
        }
    }
}