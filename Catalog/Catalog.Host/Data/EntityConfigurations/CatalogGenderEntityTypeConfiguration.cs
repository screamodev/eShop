using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogGenderEntityTypeConfiguration
    : IEntityTypeConfiguration<CatalogGender>
{
    public void Configure(EntityTypeBuilder<CatalogGender> builder)
    {
        builder.ToTable("CatalogGender");

        builder.HasKey(catalogGender => catalogGender.Id);

        builder.Property(catalogGender => catalogGender.Id)
            .UseHiLo("catalog_gender_hilo")
            .IsRequired();

        builder.Property(catalogGender => catalogGender.Gender)
            .IsRequired()
            .HasMaxLength(10);
    }
}