using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Store.Core.Builders;
using Store.Core.Models;
using Store.Infrastructure.Contracts.Dao;
using Store.Infrastructure.Repositories;

namespace UnitTests.Store.Infrastructure.Repositories;

public class UserRepositoryCachingTests
{
    private readonly UserRepository _userRepository;
    private readonly IDistributedCache _cache;
    
    public UserRepositoryCachingTests()
    {
        var userDao = new Mock<IUserDao>();

        var options = Options.Create(new MemoryDistributedCacheOptions());
        _cache = new MemoryDistributedCache(options);

        var logger = new Mock<ILogger<UserRepository>>();
        
        _userRepository = new(logger.Object, userDao.Object, _cache);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnFromCache()
    {
        // Arrange
        var user = GetDefaultUser(out var userId);
        var key = $"user:{userId}";
        
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(user));

        // Act
        var result = await _userRepository.GetByIdAsync(userId);
        
        // Assert
        result.Should().BeEquivalentTo(user);
    }
    
    [Fact]
    public async Task RegisterAsync_ShouldSaveToCache()
    {
        // Arrange
        var user = GetDefaultUser(out var userId);
        var key = $"user:{userId}";
        var serializedUser = JsonSerializer.Serialize(user);
        
        // Act
        
        await _cache.SetStringAsync(key, serializedUser);
        
        // Assert
        var userFromCache = await _cache.GetStringAsync(key);
        userFromCache.Should().Be(serializedUser);
    }
    
    private static User GetDefaultUser(out Guid userId)
    {
        var builder = new UserBuilder();
        userId = Guid.NewGuid();
        
        builder
            .WithDefault()
            .WithId(userId);

        var user = builder.Build();
        return user;
    }
}