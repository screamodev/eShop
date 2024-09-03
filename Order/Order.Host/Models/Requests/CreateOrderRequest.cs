using System.ComponentModel.DataAnnotations;
using Order.Host.Data.Entities;

namespace Order.Host.Models.Requests;

public class CreateOrderRequest
{
    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    public List<CreateOrderItemRequest>? OrderItems { get; set; }
}