using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skyline.ApplicationCore.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Infrastructure.AdminData.EntityConfigurations
{
    public class SysUserConfiguration : IEntityTypeConfiguration<SysUser>
    {
        public void Configure(EntityTypeBuilder<SysUser> builder)
        {
            //builder.ToTable("SysUsers");
        }
    }
}
