using Blocks.Domain.OrderAggregate;
using Blocks.Domain.BuyerAggregate;
using Blocks.Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Blocks.Infrastructure.EntityConfigurations;

namespace Blocks.Infrastructure
{
    public class OrderingContext : EFContext
    {
        public OrderingContext(DbContextOptions options, IMediator mediator) : base(options, mediator)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Buyer> Buyer { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
