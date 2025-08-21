using Store.Core.Contracts.Repositories;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Models;
using Microsoft.Extensions.Logging;
using Store.Core.Contracts.Users;
using Store.Core.Contracts.Security;

namespace Store.Core.Managers;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IJwtManager _jwtManager;
    private readonly ILogger<UserManager> _logger;

    public UserManager(IUserRepository userRepository, IPasswordManager passwordManager, IJwtManager jwtManager, ILogger<UserManager> logger)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _jwtManager = jwtManager;
        _logger = logger;
    }

    public async Task<User> RegisterAsync(string name, string email, string password)
    {
        _logger.LogDebug("Starting user registration");

        try
        {
            if (await _userRepository.IsEmailClaimedAsync(email))
            {
                throw new InvalidUserDataException("Email is already registered");
            }

            var passwordHash = _passwordManager.HashPassword(password);
            var user = new User(name, email);
            await _userRepository.RegisterAsync(user, passwordHash);

            _logger.LogDebug("Ending user registration");
            
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during registration");
            throw;
        }
    }

    public async Task<string> AuthenticateAsync(string email, string password)
    {
        try
        {
            _logger.LogDebug("Starting user {UserEmail} authentication", email);

            var userData = await _userRepository.GetByEmailAsync(email);

            var passwordHashFromDb = userData.passwordHash;
            _passwordManager.VerifyPassword(passwordHashFromDb, password);
            
            return _jwtManager.GenerateToken(userData.user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during authentication {UserEmail}", email);
            throw;
        }
        finally
        {
            _logger.LogDebug("Ending user {UserEmail} authentication", email);
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