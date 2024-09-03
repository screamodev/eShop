using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Models.Dtos;

public class BasketItemDto
{
    [Required] public int Id { get; set; }

    [Required] public string? Name { get; set; }

    [Required] public decimal Price { get; set; }

    public string? PictureUrl { get; set; }

    public string? Gender { get; set; }

    public string? Size { get; set; }

    [Required] public int Count { get; set; }
}