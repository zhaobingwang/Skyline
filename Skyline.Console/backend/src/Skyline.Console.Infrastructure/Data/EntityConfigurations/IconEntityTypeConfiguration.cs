using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.Infrastructure.Data.EntityConfigurations
{
    public class IconEntityTypeConfiguration : IEntityTypeConfiguration<Icon>
    {
        public void Configure(EntityTypeBuilder<Icon> builder)
        {
            builder.ToTable("Icons");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasComment("Id");

            builder.Property(i => i.Code)
                .IsRequired()
                .HasColumnType("nvarchar(50)")
                .HasComment("图标编码");

            builder.Property(i => i.Size)
                 .HasColumnType("nvarchar(20)")
                 .HasComment("图标大小(px)");

            builder.Property(i => i.Color)
                 .HasColumnType("nvarchar(50)")
                 .HasComment("图标颜色");

            builder.Property(i => i.Description)
                 .HasColumnType("nvarchar(1024)")
                 .HasComment("图标说明");
        }
    }
}
