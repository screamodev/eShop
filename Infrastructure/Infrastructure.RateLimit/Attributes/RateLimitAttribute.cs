using Infrastructure.RateLimit.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.RateLimit.Attributes;

public class RateLimitAttribute : ServiceFilterAttribute
{
    public RateLimitAttribute() : base(typeof(RateLimitFilter))
    {
    }
}