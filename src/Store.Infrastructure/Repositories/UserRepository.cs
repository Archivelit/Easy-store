namespace Store.Infrastructure.Repositories;

public sealed class UserRepository(
    ILogger<UserRepository> logger, 
    IUserDao userDao, 
    IDistributedCache cache
    ) : IUserRepository
{
    private readonly ILogger<UserRepository> _logger = logger;
    private readonly IUserDao _userDao = userDao;
    private readonly IDistributedCache _cache = cache;

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogDebug("Deleting user {UserId}", id);

        var key = $"user:{id}";

        await _cache.RemoveAsync(key);
        await _userDao.DeleteAsync(
            await _userDao.GetByIdAsync(id) 
            ?? throw new InvalidUserDataException($"User {id} not found")
        );

        _logger.LogInformation("User {UserId} was succesfuly deleted", id);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        _logger.LogDebug("Getting user {UserEmail}", email);

        var userEntity = await _userDao.GetByEmailAsync(email) 
            ?? throw new InvalidUserDataException($"User {email} not found");

        return new(userEntity);
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("User {UserId} requested", id);

        var key = $"user:{id}";

        var userFromCache = await _cache.GetStringAsync(key);
        
        if (userFromCache is not null)
        {
            return JsonSerializer.Deserialize<User>(userFromCache)!;
        }

        var user = await _userDao.GetByIdAsync(id)
            ?? throw new InvalidUserDataException($"User {id} not found");
            
        var json = JsonSerializer.Serialize(user);    
        await _cache.SetStringAsync(key, json);

        return new(user);
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

    public async Task RegisterAsync(User user)
    {
        _logger.LogDebug("Registering user {UserId}", user.Id);

        var key = $"user:{user.Id}";

        var userEntity = new UserEntity(user);

        await _userDao.RegisterAsync(userEntity);
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(userEntity));

        _logger.LogInformation("User {UserId} registered succesfuly", user.Id);
    }

    public async Task UpdateAsync(User user)
    {
        _logger.LogDebug("Updating user {UserId}", user.Id);

        var key = $"user:{user.Id}";

        var userEntity = new UserEntity(user);

        await _userDao.UpdateAsync(userEntity);
        await _cache.UpdateCacheAsync(key, JsonSerializer.Serialize(userEntity));
    }
}