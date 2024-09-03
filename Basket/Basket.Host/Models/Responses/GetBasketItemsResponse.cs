using Basket.Host.Models.Dtos;

namespace Basket.Host.Models.Responses;

public class GetBasketItemsResponse
{
    public IEnumerable<BasketItemDto> CatalogItems { get; set; } = null!;
}