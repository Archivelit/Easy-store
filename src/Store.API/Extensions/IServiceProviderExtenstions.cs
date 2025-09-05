namespace Store.API.Extensions;

public static class IServiceProviderExtenstions
{
    public static async Task<IServiceProvider> MigrateDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();

        return services;
    }
}
