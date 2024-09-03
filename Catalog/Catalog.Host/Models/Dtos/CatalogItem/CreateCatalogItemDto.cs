using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Dtos.CatalogItem;

public class CreateCatalogItemDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    public string Description { get; set; } = null!;

    [Range(0, 999999.99)]
    public decimal Price { get; set; }

    [StringLength(100)]
    public string PictureFileName { get; set; } = null!;

    public int CatalogTypeId { get; set; }

    public int CatalogBrandId { get; set; }

    [Range(0, 999999.99)]
    public int AvailableStock { get; set; }
}