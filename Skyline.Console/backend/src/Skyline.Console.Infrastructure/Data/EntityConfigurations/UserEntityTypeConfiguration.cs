using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Console.Infrastructure.Data.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Guid);

            builder.Property(u => u.Guid)
                .ValueGeneratedNever()
                .IsRequired()
                .HasDefaultValueSql("newid()")
                .HasComment("Guid");

            builder.Property(u => u.LoginName)
                .IsRequired()
                .HasColumnType("nvarchar(16)")
                .HasComment("登录账户名");

            builder.Property(u => u.NickName)
                .HasColumnType("nvarchar(16)")
                .HasComment("昵称");

            builder.Property(u => u.PasswordHash)
                .HasColumnType("nvarchar(255)")
                .HasComment("密码哈希值");

            builder.Property(u => u.Avatar)
                .HasColumnType("nvarchar(255)")
                .HasComment("头像");

            builder.Property(u => u.Description)
                .HasColumnType("nvarchar(255)")
                .HasComment("描述信息");
        }
    }
}
