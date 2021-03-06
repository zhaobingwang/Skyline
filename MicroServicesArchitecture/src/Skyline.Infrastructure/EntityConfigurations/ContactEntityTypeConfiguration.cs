﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skyline.Domain.ContactAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Infrastructure.EntityConfigurations
{
    public class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.OwnerId).HasMaxLength(64).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(32).IsRequired();
            builder.Property(c => c.Address).HasMaxLength(256);
            builder.Property(c => c.Province).HasMaxLength(32);
            builder.Property(c => c.City).HasMaxLength(32);
            builder.Property(c => c.Zip).HasMaxLength(10);
            builder.Property(c => c.Email).HasMaxLength(256);
        }
    }
}
