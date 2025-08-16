using StackExchange.Redis;

namespace Store.Infrastructure.Data.Redis;

internal static class Cache
{
    public static IDatabase Database;

    static Cache()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        Database = redis.GetDatabase();
    }
}