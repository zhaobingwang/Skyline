using Blocks.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blocks.Infrastructure.EntityConfigurations
{
    class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable("Orders");
            builder.Property(o => o.BuyerId).HasMaxLength(20);
            builder.Property(o => o.BuyerName).HasMaxLength(32);
            builder.OwnsOne(o => o.Address, a =>
            {
                a.WithOwner();
                a.Property(p => p.City).HasMaxLength(20);
                a.Property(p => p.Street).HasMaxLength(50);
                a.Property(p => p.ZipCode).HasMaxLength(10);
            });
        }
    }
}
