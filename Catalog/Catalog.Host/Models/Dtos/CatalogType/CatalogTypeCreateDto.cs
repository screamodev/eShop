using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Dtos.CatalogType;

public class CatalogTypeCreateDto
{
    [Required]
    [StringLength(100)]
    public string? Type { get; set; }
}