using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace IntegrationTests;

public class TestContainersFixture : IAsyncDisposable
{
    public PostgreSqlContainer Postgres { get; private set; }
    public RedisContainer Redis { get; private set; }

    public async Task InitializeAsync()
    {
        Postgres = new PostgreSqlBuilder()
            .WithImage("postgres:16")
            .WithDatabase("testdb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

        Redis = new RedisBuilder()
            .WithImage("redis:8.2")
            .Build();

        await Postgres.StartAsync();
        await Redis.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await Postgres.DisposeAsync();
        await Redis.DisposeAsync();
    }
}