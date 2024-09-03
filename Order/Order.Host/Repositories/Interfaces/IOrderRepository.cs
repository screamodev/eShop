using Order.Host.Data.Entities;
using Order.Host.Models.Requests;

namespace Order.Host.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<List<OrderEntity>> GetAllByUserIdAsync(int userId);
    Task<OrderEntity?> AddAsync(OrderEntity order);
}