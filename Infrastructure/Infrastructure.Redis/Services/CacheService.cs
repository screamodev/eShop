using Infrastructure.Redis.Configurations;
using Infrastructure.Redis.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Infrastructure.Redis.Services
{
    public class CacheService : ICacheService
    {
        private readonly ILogger<CacheService> _logger;
        private readonly IRedisCacheConnectionService _redisCacheConnectionService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly RedisConfig _config;

        public CacheService(
            ILogger<CacheService> logger,
            IRedisCacheConnectionService redisCacheConnectionService,
            IOptions<RedisConfig> config,
            IJsonSerializer jsonSerializer)
        {
            _logger = logger;
            _redisCacheConnectionService = redisCacheConnectionService;
            _jsonSerializer = jsonSerializer;
            _config = config.Value;
        }

        public Task AddOrUpdateAsync<T>(string key, T value, IDatabase redis, TimeSpan? expiry) 
        => AddOrUpdateInternalAsync(key, value, redis, expiry);

        public async Task<T> GetAsync<T>(string key)
        {
            var redis = GetRedisDatabase();

            var cacheKey = GetItemCacheKey(key);

            var serialized = await redis.StringGetAsync(cacheKey);
            
            return serialized.HasValue ? 
                _jsonSerializer.Deserialize<T>(serialized.ToString()) 
                : default(T)!;
        }

        private string GetItemCacheKey(string userId) =>
            $"{userId}";

        private async Task AddOrUpdateInternalAsync<T>(string key, T value,
            IDatabase redis, TimeSpan? expiry)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? TimeSpan.FromMinutes(2);

            var cacheKey = GetItemCacheKey(key);
            var serialized = _jsonSerializer.Serialize(value);

            if (await redis.StringSetAsync(cacheKey, serialized, expiry))
            {
                _logger.LogInformation($"Cached value for key {key} cached");
            }
            else
            {
                _logger.LogInformation($"Cached value for key {key} updated");
            }
        }

        private IDatabase GetRedisDatabase() => _redisCacheConnectionService.Connection.GetDatabase();
    }
}