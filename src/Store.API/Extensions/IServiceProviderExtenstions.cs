namespace Store.API.Extensions;

public static class IServiceProviderExtenstions
{
    /// <summary>
    /// Add migrations to database if they are not applied
    /// </summary>
    public static async Task<IServiceProvider> MigrateDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();

        return services;
    }
}