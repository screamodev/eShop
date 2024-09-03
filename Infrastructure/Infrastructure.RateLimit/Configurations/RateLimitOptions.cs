namespace Infrastructure.RateLimit.Configurations;

public class RateLimitOptions
{
    public int AllowedRequestsCount { get; set; }
    public TimeSpan TimeLimit { get; set; }
}