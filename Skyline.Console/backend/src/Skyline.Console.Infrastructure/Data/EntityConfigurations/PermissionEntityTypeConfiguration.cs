using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.Infrastructure.Data.EntityConfigurations
{
    public class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(p => p.Code);

            builder.HasIndex(p => p.Code)
                .IsUnique();

            builder.HasOne(p => p.Menu)
                .WithMany(p => p.Permissions)
                .HasForeignKey(p => p.MenuGuid);


            builder.Property(p => p.Code)
                .IsRequired()
                .HasColumnType("nvarchar(20)")
                .HasComment("权限编码");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("nvarchar(50)")
                .HasComment("权限编码");

            builder.Property(p => p.ActionCode)
                .IsRequired()
                .HasColumnType("nvarchar(80)")
                .HasComment("权限编码");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("nvarchar(max)")
                .HasComment("描述信息");
        }
    }
}
