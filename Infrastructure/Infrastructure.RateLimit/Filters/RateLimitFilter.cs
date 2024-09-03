using System.Net;
using Infrastructure.Exceptions;
using Infrastructure.RateLimit.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.RateLimit.Filters;

public class RateLimitFilter : IAsyncActionFilter
{
    private readonly IRateLimitService _rateLimitService;

    public RateLimitFilter(IRateLimitService rateLimitService)
    {
        _rateLimitService = rateLimitService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var requestsCounterKey = _rateLimitService.GenerateRequestCounterKey(context.HttpContext);

        if (await _rateLimitService.IsRequestAllowedAsync(requestsCounterKey))
        {
            await _rateLimitService.IncrementRequestCountAsync(requestsCounterKey);
            await next();
        }
        else
        {
            context.Result = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.TooManyRequests,
                Content = new BusinessException("Too many requests from the current IP.").ToString()
            };
        }
    }
}
