using System.Net;
using Infrastructure.Exceptions;
using Infrastructure.RateLimit.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.RateLimit.Middlewares;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRateLimitService _rateLimitService;

    public RateLimitMiddleware(RequestDelegate next, IRateLimitService rateLimitService)
    {
        _next = next;
        _rateLimitService = rateLimitService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var requestsCounterKey = _rateLimitService.GenerateRequestCounterKey(context);

        if (await _rateLimitService.IsRequestAllowedAsync(requestsCounterKey))
        {
            await _rateLimitService.IncrementRequestCountAsync(requestsCounterKey);
            await _next(context);
        }
        else
        {
            await HandleExceptionAsync(context);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
        return context.Response.WriteAsync(new BusinessException(
            "Too many requests from the current IP.").ToString());
    }
}

public static class RateLimitMiddlewareExtension
{
    public static IApplicationBuilder UseRateLimit(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RateLimitMiddleware>();
    }
}


