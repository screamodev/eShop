using Basket.Host.Models.Dtos;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services.Interfaces;
using Infrastructure.Redis.Services.Interfaces;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;

    public BasketService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }
    
    public async Task AddItem(string basketId, AddBasketItemRequest data)
    {
            var filteredData = data.CatalogItems
                .GroupBy(item => item.Id)
                .Select(group => new BasketItemDto()
                {
                    Id = group.Key,
                    Name = group.First().Name,
                    Price = group.First().Price,
                    PictureUrl = group.First().PictureUrl,
                    Gender = group.First().Gender,
                    Size = group.First().Size,
                    Count = group.Sum(item => item.Count)
                })
                .ToList();

            await _cacheService.AddOrUpdateAsync(basketId, new GetBasketItemsResponse
            {
                CatalogItems = filteredData
            });
    }


    public async Task<GetBasketItemsResponse?> GetItems(string basketId)
    {
        var result = await _cacheService.GetAsync<GetBasketItemsResponse>(basketId);
        return result;
    }
}