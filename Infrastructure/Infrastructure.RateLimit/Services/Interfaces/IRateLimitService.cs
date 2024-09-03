using Microsoft.AspNetCore.Http;

namespace Infrastructure.RateLimit.Services.Interfaces;

public interface IRateLimitService
{
    Task<bool> IsRequestAllowedAsync(string key);
    Task IncrementRequestCountAsync(string key);

    string GenerateRequestCounterKey(HttpContext context);
}