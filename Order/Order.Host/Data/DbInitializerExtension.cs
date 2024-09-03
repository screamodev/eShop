using Order.Host.Data;

namespace Catalog.Host.Data;

internal static class DbInitializerExtension
{
    public static async Task<IApplicationBuilder> UseItToSeedSqlServer(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            await DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return app;
    }
}