using Catalog.Host.Models.Dtos.CatalogBrand;
using Catalog.Host.Models.Dtos.CatalogGender;
using Catalog.Host.Models.Dtos.CatalogSize;
using Catalog.Host.Models.Dtos.CatalogType;

namespace Catalog.Host.Models.Dtos.CatalogItem;

public class CatalogItemDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? PictureUrl { get; set; }

    public CatalogTypeDto? CatalogType { get; set; }

    public CatalogBrandDto? CatalogBrand { get; set; }

    public CatalogGenderDto? CatalogGender { get; set; }

    public List<CatalogSizeDto>? CatalogItemSizes { get; set; }

    public int AvailableStock { get; set; }
}
