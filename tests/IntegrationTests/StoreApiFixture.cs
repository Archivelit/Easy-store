using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data;

namespace IntegrationTests;

public sealed class StoreApiFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
#nullable disable
    private PostgreSqlContainer _postgres;
    private string _postgresConnectionString;
    private RedisContainer _redis;
    private string _redisConnectionString;
#nullable enable

    public async ValueTask InitializeAsync()
    {
        _postgres = InitPostgresContainer();
        _redis = InitRedisContainer();

        _postgres.StartAsync().GetAwaiter().GetResult();
        _redis.StartAsync().GetAwaiter().GetResult();

        _postgresConnectionString = _postgres.GetConnectionString();
        _redisConnectionString = _redis.GetConnectionString();
    }
    public new async Task DisposeAsync()
    {
        await _postgres.StopAsync();
        await _redis.StopAsync();

        await base.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                [ "ConnectionStrings:DefaultConnection" ] = _postgresConnectionString,
                [ "ConnectionStrings:RedisConnection" ] = _redisConnectionString 
            });
        });
    }

    private PostgreSqlContainer InitPostgresContainer()
    {
        return new PostgreSqlBuilder()
            .WithImage("ankane/pgvector:latest")
            .WithDatabase("TestDb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
    }

    private RedisContainer InitRedisContainer()
    {
        return new RedisBuilder()
            .WithImage("redis:latest")
            .Build();
    }
}