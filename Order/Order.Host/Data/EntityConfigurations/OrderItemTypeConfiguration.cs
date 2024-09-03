using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EntityConfigurations;

public class OrderItemTypeConfiguration
    : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");

        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Id)
            .UseHiLo("order_item_hilo")
            .IsRequired();

        builder.Property(oi => oi.Name)
            .IsRequired();
        
        builder.Property(oi => oi.Price)
            .IsRequired();

        builder.Property(oi => oi.PictureUrl)
            .IsRequired(false);
        
        builder.Property(oi => oi.Size)
            .IsRequired(false);

        builder.Property(oi => oi.Gender)
            .IsRequired(false);

        builder.Property(oi => oi.CatalogItemId)
            .IsRequired();

        builder.Property(oi => oi.Count)
            .IsRequired();
    }
}