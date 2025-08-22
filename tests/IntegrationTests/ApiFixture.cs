using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace IntegrationTests;

public class ApiFixture : IAsyncLifetime
{
    public PostgreSqlContainer Postgres { get; private set; }
    public RedisContainer Redis { get; private set; }
    public ApiFactory Factory { get; private set; }

    public async ValueTask InitializeAsync()
    {
        Postgres = new PostgreSqlBuilder()
            .WithImage("postgres:16")
            .WithDatabase("test_db")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

        Redis = new RedisBuilder()
            .WithImage("redis:8.2")
            .Build();
        
        Postgres.StartAsync().GetAwaiter().GetResult();
        Redis.StartAsync().GetAwaiter().GetResult();

        Factory = new(Postgres, Redis);
    }
    
    public async ValueTask DisposeAsync()
    {
        await Factory.DisposeAsync();
        await Postgres.DisposeAsync();
        await Redis.DisposeAsync();
    }
}