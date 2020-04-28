using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skyline.Tools.SeedData.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Tools.SeedData.Data.EntityConfigurations
{
    public class TmpEntityTypeConfiguration : IEntityTypeConfiguration<Tmp>
    {
        public void Configure(EntityTypeBuilder<Tmp> builder)
        {
            builder.ToTable("tmp");
            builder.HasKey(x => x.id);
            builder.Property(x => x.remark).HasMaxLength(1024);
        }
    }
}
