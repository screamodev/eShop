using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Dtos.CatalogBrand;

public class CatalogBrandCreateDto
{
    [Required]
    [StringLength(100)]
    public string? Brand { get; set; }
}