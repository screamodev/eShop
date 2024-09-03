using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EntityConfigurations;

public class OrderEntityTypeConfiguration
    : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("Order");

        builder.HasKey(oe => oe.Id);

        builder.Property(oe => oe.Id)
            .UseHiLo("order_hilo")
            .IsRequired();

        builder.Property(oe => oe.OrderDate)
            .IsRequired();

        builder.Property(oe => oe.TotalAmount)
            .IsRequired();

        builder.Property(oe => oe.CustomerId)
            .IsRequired();

        builder.HasMany(oe => oe.OrderItems)
            .WithOne(oi => oi.Order);
    }
}