using StackExchange.Redis;

namespace Infrastructure.Redis.Services.Interfaces;

public interface ICacheService
{
    Task AddOrUpdateAsync<T>(string key, T value, IDatabase redis = null!, TimeSpan? expiry = null);

    Task<T> GetAsync<T>(string key);
}