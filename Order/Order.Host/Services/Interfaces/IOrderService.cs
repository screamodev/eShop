using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;

namespace Order.Host.Services.Interfaces;

public interface IOrderService
{
    Task<List<OrderDto>> GetAllByUserIdAsync(string userId);
    Task<OrderDto?> AddAsync(CreateOrderRequest createOrder, int customerId);
}