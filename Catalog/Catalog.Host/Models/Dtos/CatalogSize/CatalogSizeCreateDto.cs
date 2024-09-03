using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Dtos.CatalogSize;

public class CatalogSizeCreateDto
{
    [Required]
    [StringLength(10)]
    public string? Size { get; set; }
}