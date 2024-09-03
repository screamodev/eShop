using Basket.Host.Models.Dtos;

namespace Basket.Host.Models.Requests;

public class AddBasketItemRequest
{
    public IEnumerable<BasketItemDto> CatalogItems { get; set; } = null!;
}