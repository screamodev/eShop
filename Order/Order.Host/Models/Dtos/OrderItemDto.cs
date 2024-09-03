#pragma warning disable CS8618

namespace Order.Host.Models.Dtos;

public class OrderItemDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public string? PictureUrl { get; set; }
    
    public string? Gender { get; set; }

    public string? Size { get; set; }

    public int CatalogItemId { get; set; }
    
    public int Count { get; set; }
}