using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.Infrastructure.Data.EntityConfigurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(r => r.Code);
            builder.HasIndex(r => r.Code)
                .IsUnique();

            builder.Property(r => r.Code)
                .IsRequired()
                .HasColumnType("nvarchar(32)")
                .HasComment("角色编码");

            builder.Property(r => r.Name)
                .IsRequired()
                .HasColumnType("nvarchar(32)")
                .HasComment("角色名称");

            builder.Property(r => r.Description)
                .HasColumnType("nvarchar(1024)")
                .HasComment("角色描述");
        }
    }
}
