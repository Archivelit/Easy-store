using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Store.API;

namespace IntegrationTests;

public sealed class StoreApiFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private IHost? _app;
    private PostgreSqlContainer _postgres;
    private string? _postgresConnectionString;
    private RedisContainer _redis;
    private string? _redisConnectionString;
    
    public StoreApiFixture()
    {
        _postgres = InitPostgresContainer();
        _redis = InitRedicContainer();
    }

    public async ValueTask InitializeAsync()
    {
        await _postgres.StartAsync();
        await _redis.StartAsync();

        _postgresConnectionString = _postgres.GetConnectionString();
        _redisConnectionString = _redis.GetConnectionString();

        _app = CreateHostBuilder()?.Build();

        await _app!.StartAsync().ConfigureAwait(false);
    }
    public new async Task DisposeAsync()
    {
        if (_app is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync().ConfigureAwait(false);
        }
        else
        {
            _app?.Dispose();
        }

        await _postgres.StopAsync();
        await _redis.StopAsync();

        await base.DisposeAsync();
    }

    protected override IHostBuilder CreateHostBuilder()
    {
    #pragma warning disable CS8620, CS8604
        return base.CreateHostBuilder()!
            .ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:DefaultConnection", _postgresConnectionString },
                { "ConnectionStrings:RedisConnection", _redisConnectionString }
            });
        });
    #pragma warning restore
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

    private RedisContainer InitRedicContainer()
    {
        return new RedisBuilder()
            .WithImage("redis:latest")
            .Build();
    }
}