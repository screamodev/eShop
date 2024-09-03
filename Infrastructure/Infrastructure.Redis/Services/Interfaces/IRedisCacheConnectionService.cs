using StackExchange.Redis;

namespace Infrastructure.Redis.Services.Interfaces
{
    public interface IRedisCacheConnectionService
    {
        public IConnectionMultiplexer Connection { get; }
    }
}