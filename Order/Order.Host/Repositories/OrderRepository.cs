using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<OrderRepository> _logger;

    public OrderRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<OrderRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<List<OrderEntity>> GetAllByUserIdAsync(int userId)
    {
        var orders = await _dbContext.Orders
            .Include(e => e.OrderItems)
            .Where(e => e.CustomerId == userId)
            .ToListAsync();

        return orders;
    }

    public async Task<OrderEntity?> AddAsync(OrderEntity order)
    {
        var result = await _dbContext.AddAsync(order);

        if (result == null)
        {
            return null;
        }

        await _dbContext.SaveChangesAsync();

        return result.Entity;
    }
}