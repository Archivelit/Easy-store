using Store.Core.Contracts.Repositories;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Models;
using Microsoft.Extensions.Logging;
using Store.Core.Contracts.Users;

namespace Store.Core.Managers;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserManager> _logger;

    public UserManager(IUserRepository userRepository, ILogger<UserManager> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<User> RegisterAsync(string name, string email)
    {
        _logger.LogDebug("Starting user registration");

        try
        {
            if (await _userRepository.IsEmailClaimedAsync(email))
            {
                throw new InvalidUserDataException("Email is already registered");
            }

            var user = new User(name, email);
            await _userRepository.RegisterAsync(user);

            _logger.LogDebug("Ending user registration");
            
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during registration");
            throw;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Deleting user {UserId}", id);
            await _userRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during deleting user {UserId}", id);
            throw;
        }
    }
}