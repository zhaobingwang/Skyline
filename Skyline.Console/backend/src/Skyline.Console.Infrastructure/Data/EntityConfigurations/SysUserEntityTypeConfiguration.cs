using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Console.Infrastructure.Data.EntityConfigurations
{
    public class SysUserEntityTypeConfiguration : IEntityTypeConfiguration<SysUser>
    {
        public void Configure(EntityTypeBuilder<SysUser> builder)
        {
            builder.ToTable("SysUsers");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).ValueGeneratedNever().HasDefaultValue("newid()").HasComment("Guid");
            builder.Property(u => u.LoginName).HasMaxLength(50).HasComment("登录账户名");
        }
    }
}
