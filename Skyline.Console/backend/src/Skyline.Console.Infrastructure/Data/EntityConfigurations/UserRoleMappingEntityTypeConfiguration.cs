using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.Infrastructure.Data.EntityConfigurations
{
    public class UserRoleMappingEntityTypeConfiguration : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.HasKey(x => new
            {
                x.UserGuid,
                x.RoleCode
            });

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserRoleMappings)
                .HasForeignKey(x => x.UserGuid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleCode)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
