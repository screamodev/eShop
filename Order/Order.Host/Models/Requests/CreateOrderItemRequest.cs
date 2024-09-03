#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace Order.Host.Models.Requests;

public class CreateOrderItemRequest
{
    [Required]
    public string Name { get; set; }
    
    public string? Gender { get; set; }
    
    public string? Size { get; set; }

    public int Count { get; set; }
    
    public int CatalogItemId { get; set; }
}