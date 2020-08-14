using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.Infrastructure.Data.EntityConfigurations
{
    public class MenuEntityTypeConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");
            builder.HasKey(m => m.Guid);

            builder.Property(m => m.Guid)
                .ValueGeneratedNever()
                .IsRequired()
                .HasDefaultValueSql("newid()")
                .HasComment("Guid");

            builder.Property(m => m.Name)
                .IsRequired()
                .HasColumnType("nvarchar(50)")
                .HasComment("菜单名称");

            builder.Property(m => m.Url)
                .HasColumnType("nvarchar(255)")
                .HasComment("链接地址");

            builder.Property(m => m.Alias)
               .HasColumnType("nvarchar(255)")
               .HasComment("页面别名");

            builder.Property(m => m.Url)
               .HasColumnType("nvarchar(128)")
               .HasComment(" 菜单图标(可选)");

            builder.Property(m => m.Description)
               .HasColumnType("nvarchar(1000)")
               .HasComment("描述信息");
        }
    }
}
