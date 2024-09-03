using AutoMapper;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Order.Host.Configurations;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Services;
using Order.Host.Models.Responses;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Order.Host.Services;

public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IOptions<OrderConfig> _settings;
    private readonly HttpClient _httpClient;

    public OrderService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderRepository orderRepository,
        IMapper mapper,
        IOptions<OrderConfig> settings,
        HttpClient httpClient)
        : base(dbContextWrapper, logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _settings = settings;
        _httpClient = httpClient;
    }

    public async Task<List<OrderDto>> GetAllByUserIdAsync(string userId)
    {
        var orders = await _orderRepository.GetAllByUserIdAsync(int.Parse(userId));

        var orderDtos = _mapper.Map<List<OrderDto>>(orders);

        return orderDtos;
    }

    public async Task<OrderDto?> AddAsync(CreateOrderRequest createOrder, int customerId)
    {
        var validOrderItems = new List<OrderItem>();

        foreach (var item in createOrder.OrderItems)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{_settings.Value.CatalogUrl}/itemById?id={item.CatalogItemId}");
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var catalogItem = JsonSerializer.Deserialize<CatalogItemResponse>(responseContent);

                if (catalogItem != null)
                {

                    validOrderItems.Add(new OrderItem
                    {
                        Name = catalogItem.Name,
                        Price = catalogItem.Price,
                        PictureUrl = catalogItem.PictureFileName,
                        CatalogItemId = item.CatalogItemId,
                        Size = item.Size,
                        Gender = item.Gender,
                        Count = item.Count
                    });
                }
            }
        }

        if (validOrderItems.Count == 0)
        {
            return null;
        }

        var orderEntity = new OrderEntity
        {
            OrderDate = createOrder.OrderDate,
            CustomerId = customerId,
            OrderItems = validOrderItems,
            TotalAmount = validOrderItems.Sum(x => x.Count * x.Price)
        };

        orderEntity = await _orderRepository.AddAsync(orderEntity);

        return _mapper.Map<OrderDto>(orderEntity);
    }
}