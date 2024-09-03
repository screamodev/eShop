using Order.Host.Data;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        await context.Database.EnsureCreatedAsync();
    }
}