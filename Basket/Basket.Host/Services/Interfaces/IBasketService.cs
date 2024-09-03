using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task AddItem(string basketId, AddBasketItemRequest data);
    Task<GetBasketItemsResponse?> GetItems(string basketId);
}