using Infrastructure.RateLimit.Configurations;
using Infrastructure.RateLimit.Services.Interfaces;
using Infrastructure.Redis.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.RateLimit.Services;

public class RateLimitService : IRateLimitService
{
    private readonly ICacheService _cacheService;
    private readonly IOptions<RateLimitOptions> _options;

    public RateLimitService(ICacheService cacheService, IOptions<RateLimitOptions> options)
    {
        _cacheService = cacheService;
        _options = options;
    }

    public async Task<bool> IsRequestAllowedAsync(string key)
    {
        var requestsCounter = await GetRequestsCounterAsync(key);
        return requestsCounter < _options.Value.AllowedRequestsCount;
    }

    public async Task IncrementRequestCountAsync(string key)
    {
        var requestsCounter = await GetRequestsCounterAsync(key);
        requestsCounter++;
        await _cacheService.AddOrUpdateAsync(key, requestsCounter, null!, _options.Value.TimeLimit);
    }

    public string GenerateRequestCounterKey(HttpContext context)
    {
        var userIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown-ip";
        var requestedEndpoint = context.GetEndpoint()?.DisplayName ?? "unknown-endpoint";
        return $"{userIp}:{requestedEndpoint}";
    }

    private async Task<int> GetRequestsCounterAsync(string key)
    {
        var cachedValue = await _cacheService.GetAsync<string>(key);
        return int.TryParse(cachedValue, out var requestsCounter) ? requestsCounter : 0;
    }
}