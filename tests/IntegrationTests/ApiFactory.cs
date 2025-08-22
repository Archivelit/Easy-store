using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Store.API;
using Store.Infrastructure.Data;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace IntegrationTests;

public class ApiFactory: WebApplicationFactory<Program>
{
    private readonly PostgreSqlContainer _postgresContainer;
    private readonly RedisContainer _redisContainer;
    
    public ApiFactory(PostgreSqlContainer postgresContainer, RedisContainer redisContainer)
    {
        _redisContainer = redisContainer;
        _postgresContainer = postgresContainer;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.RemoveAll<IDistributedCache>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_postgresContainer.GetConnectionString()));
            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = _redisContainer.GetConnectionString();
            });
        });
    }
}