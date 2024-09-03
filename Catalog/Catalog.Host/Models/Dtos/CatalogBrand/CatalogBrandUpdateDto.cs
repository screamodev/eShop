using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Dtos.CatalogBrand;

public class CatalogBrandUpdateDto
{
    [Required]
    [StringLength(100)]
    public string? Brand { get; set; }
}