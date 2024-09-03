using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Dtos.CatalogGender;

public class CatalogGenderCreateDto
{
    [Required]
    [StringLength(10)]
    public string? Gender { get; set; }
}