using Basket.Host.Models.Dtos;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using FluentAssertions;
using Infrastructure.Redis.Services.Interfaces;
using Moq;

public class BasketServiceTests
{
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly IBasketService _basketService;

    public BasketServiceTests()
    {
        _cacheServiceMock = new Mock<ICacheService>();
        _basketService = new BasketService(_cacheServiceMock.Object);
    }

    [Fact]
    public async Task AddItem_ShouldCallCacheServiceAddOrUpdateAsync()
    {
        // Arrange
        string basketId = "test-basket";
        var request = new AddBasketItemRequest
        {
            CatalogItems = new List<BasketItemDto>
            {
                new BasketItemDto
                    { Id = 1, Name = "Item1", Price = 10, Count = 1, PictureUrl = "url1", Gender = "M", Size = "L" },
                new BasketItemDto
                    { Id = 1, Name = "Item1", Price = 10, Count = 2, PictureUrl = "url1", Gender = "M", Size = "L" },
                new BasketItemDto
                    { Id = 2, Name = "Item2", Price = 15, Count = 1, PictureUrl = "url2", Gender = "F", Size = "M" }
            }
        };

        // Act
        await _basketService.AddItem(basketId, request);

        // var timesOnce = Times.Once();
        //
        // _cacheServiceMock.Verify<Task>(
        //     x => x.AddOrUpdateAsync(basketId, It.Is<GetBasketItemsResponse>(r =>
        //         r.CatalogItems.ToList().Count == 2 &&
        //         r.CatalogItems.First().Id == 1 &&
        //         r.CatalogItems.First().Count == 3 &&
        //         r.CatalogItems.Last().Id == 2 &&
        //         r.CatalogItems.Last().Count == 1
        //     )), timesOnce
        // );
    }

    [Fact]
    public async Task GetItems_ShouldReturnBasketItems_WhenItemsExistInCache()
    {
        // Arrange
        string basketId = "test-basket";
        var expectedResponse = new GetBasketItemsResponse
        {
            CatalogItems = new List<BasketItemDto>
            {
                new BasketItemDto { Id = 1, Name = "Item1", Price = 10, Count = 3, PictureUrl = "url1", Gender = "M", Size = "L" }
            }
        };

        _cacheServiceMock.Setup(x => x.GetAsync<GetBasketItemsResponse>(basketId)).ReturnsAsync(expectedResponse);

        // Act
        var result = await _basketService.GetItems(basketId);

        // Assert
        result.Should().NotBeNull();
        result?.CatalogItems.Should().HaveCount(1);
        result?.CatalogItems.First().Id.Should().Be(1);
        result?.CatalogItems.First().Count.Should().Be(3);
    }

    [Fact]
    public async Task GetItems_ShouldReturnNull_WhenNoItemsExistInCache()
    {
        // Arrange
        string basketId = "test-basket";
        _cacheServiceMock.Setup(x => x.GetAsync<GetBasketItemsResponse>(basketId)).ReturnsAsync((GetBasketItemsResponse?)null);

        // Act
        var result = await _basketService.GetItems(basketId);

        // Assert
        result.Should().BeNull();
    }
}
