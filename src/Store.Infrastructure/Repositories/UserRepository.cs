using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Store.Core.Contracts.Repositories;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Models;
using Store.Infrastructure.Data.DataAccessObjects;
using Store.Infrastructure.Entities;
using Store.Infrastructure.Extensions;
using Store.Infrastructure.Mappers;
using System.Text.Json;

namespace Store.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ILogger<UserRepository> _logger;
    private readonly IUserDao _userDao;
    private readonly IDistributedCache _cache;

    public UserRepository(ILogger<UserRepository> logger, IUserDao userDao, IDistributedCache cache)
    {
        _logger = logger;
        _userDao = userDao;
        _cache = cache;
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogDebug("Deleting user {UserId}", id);

        var key = $"user:{id}";

        await _cache.RemoveAsync(key);
        await _userDao.DeleteAsync(
            await _userDao.GetByIdAsync(id) 
            ?? throw new InvalidUserDataException($"User {id} not found"));

        _logger.LogInformation("User {UserId} was succesfuly deleted", id);
    }

    public async Task<(User user, string passwordHash)> GetByEmailAsync(string email)
    {
        _logger.LogDebug("Getting user {UserEmail}", email);

        var userEntity = await _userDao.GetByEmailAsync(email);

        return (UserMapper.ToDomain(userEntity), userEntity.PasswordHash);
    }

    public async Task<(User user, string passwordHash)> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Getting user {UserId}", id);

        var key = $"user:{id}";
        var entityFromCache = await _cache.GetStringAsync(key);
        UserEntity userEntity;

        if (entityFromCache is null)
        {
            userEntity = await _userDao.GetByIdAsync(id) 
                ?? throw new InvalidUserDataException("User not found");
            
            var json = JsonSerializer.Serialize(userEntity);
            
            await _cache.SetStringAsync(key, json);
        }
        else
        {
            userEntity = JsonSerializer.Deserialize<UserEntity>(entityFromCache);
        }

        return (UserMapper.ToDomain(userEntity), userEntity.PasswordHash);
    }

    public async Task<bool> IsEmailClaimedAsync(string email)
    {
        _logger.LogDebug("Checking email {Email}", email);

        return await _userDao.IsEmailClaimedAsync(email);
    }

    public async Task<bool> IsExistsAsync(Guid id)
    {
        _logger.LogDebug("Checking user {UserId}", id);

        var key = $"user:{id}";
        var user = await _cache.GetAsync(key);

        if (user is not null)
        {
            return true;
        }

        return await _userDao.IsExistsAsync(id);
    }

    public async Task RegisterAsync(User user, string passwordHash)
    {
        _logger.LogDebug("Registering user {UserId}", user.Id);

        var key = $"user:{user.Id}";

        var userEntity = UserMapper.ToEntity(user, passwordHash);

        await _userDao.RegisterAsync(userEntity);
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(userEntity));

        _logger.LogInformation("User {UserId} registered succesfuly", user.Id);
    }

    public async Task UpdateAsync(User user, string passwordHash)
    {
        _logger.LogDebug("Updating user {UserId}", user.Id);

        var key = $"user:{user.Id}";

        var userEntity = UserMapper.ToEntity(user, passwordHash);

        await _userDao.UpdateAsync(userEntity);
        await _cache.UpdateCacheAsync(key, JsonSerializer.Serialize(userEntity));
    }
}