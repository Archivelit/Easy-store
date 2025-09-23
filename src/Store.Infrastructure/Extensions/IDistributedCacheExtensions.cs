namespace Store.Infrastructure.Extensions;

public static class IDistributedCacheExtensions
{
    public static async Task UpdateCacheAsync(this IDistributedCache cache, string key, string value)
    {
        await cache.RemoveAsync(key);
        await cache.SetStringAsync(key, value);
    }

    public static async Task AddToCache(this IDistributedCache cache, string key, string value)
    {
        
    }
}