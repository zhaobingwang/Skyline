using MediatR;
using Microsoft.EntityFrameworkCore;
using Skyline.Domain.ContactAggregate;
using Skyline.Infrastructure.Core;
using Skyline.Infrastructure.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Infrastructure
{
    public class ApplicationDbContext : EFContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
        {
        }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContactEntityTypeConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
