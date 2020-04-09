using Skyline.Domain.OrderAggregate;
using Skyline.Domain.BuyerAggregate;
using Skyline.Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Skyline.Infrastructure.EntityConfigurations;

namespace Skyline.Infrastructure
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
