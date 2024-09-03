using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogSizeEntityTypeConfiguration
    : IEntityTypeConfiguration<CatalogSize>
{
    public void Configure(EntityTypeBuilder<CatalogSize> builder)
    {
        builder.ToTable("CatalogSize");

        builder.HasKey(catalogSize => catalogSize.Id);

        builder.Property(catalogSize => catalogSize.Id)
            .UseHiLo("catalog_size_hilo")
            .IsRequired();

        builder.Property(catalogSize => catalogSize.Size)
            .IsRequired()
            .HasMaxLength(5);
    }
}