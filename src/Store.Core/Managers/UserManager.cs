using System.Security.Authentication;
using Store.App.GraphQl.Users;
using Store.Core.Contracts.Repositories;
using Store.App.GraphQl.Security;
using Store.App.GraphQl.Validation;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Models;
using Bcrypt = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Logging;

namespace Store.Core.Managers;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly IUserValidator _validationService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtManager _jwtManager;
    private readonly ILogger<UserManager> _logger;

    public UserManager(IUserRepository userRepository, IUserValidator validationService,
        IPasswordHasher passwordHasher, IJwtManager jwtManager, ILogger<UserManager> logger)
    {
        _userRepository = userRepository;
        _validationService = validationService;
        _passwordHasher = passwordHasher;
        _jwtManager = jwtManager;
        _logger = logger;
    }

    public async Task<User> RegisterAsync(string name, string email, string password)
    {
        _logger.LogDebug("Starting user registration");

        try
        {
            _validationService.ValidateAndThrow(name, email, password);

            if (await _userRepository.IsEmailClaimedAsync(email))
                throw new InvalidUserDataException("Email is already registered");

            var passwordHash = _passwordHasher.HashPassword(password);

            var customer = new User(name, email);

            await _userRepository.RegisterAsync(customer, passwordHash);

            _logger.LogDebug("Ending user registration");
            
            return customer;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during registration");
            throw;
        }
    }

    public async Task<string> AuthenticateAsync(string email, string password)
    {
        try {
            _logger.LogInformation("Starting user {UserEmail} authentication", email);

            _validationService.ValidateEmail(email);
            _validationService.ValidatePassword(password);

            var userData = await _userRepository.GetByEmailAsync(email);

            var passwordHashFromDb = userData.passwordHash;

            var passwordHash = _passwordHasher.HashPassword(password);

            if (!Bcrypt.Verify(passwordHash, passwordHashFromDb))
                throw new AuthenticationException("The password doesn't match");

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
            if (id == Guid.Empty)
                throw new InvalidUserDataException("Id cannot be empty");

            await _userRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during deleting user {UserId}", id);
            throw;
        }
    }
}