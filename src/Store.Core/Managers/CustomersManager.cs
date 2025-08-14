using System.Security.Authentication;
using Store.App.GraphQl.Customers;
using Store.Core.Contracts.Repositories;
using Store.App.GraphQl.Security;
using Store.App.GraphQl.Validation;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Models;
using Bcrypt = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Logging;

namespace Store.Core.Managers;

public class CustomersManager : ICustomerManager
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerValidator _validationService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtManager _jwtManager;
    private readonly ILogger<CustomersManager> _logger;

    public CustomersManager(ICustomerRepository customerRepository, ICustomerValidator validationService,
        IPasswordHasher passwordHasher, IJwtManager jwtManager, ILogger<CustomersManager> logger)
    {
        _customerRepository = customerRepository;
        _validationService = validationService;
        _passwordHasher = passwordHasher;
        _jwtManager = jwtManager;
        _logger = logger;
    }

    public async Task<Customer> RegisterAsync(string name, string email, string password)
    {
        _logger.LogDebug("Starting user registration");

        try
        {
            _validationService.ValidateAndThrow(name, email, password);

            if (await _customerRepository.IsEmailClaimedAsync(email))
                throw new InvalidEmail("Email is already registered");

            var passwordHash = _passwordHasher.HashPassword(password);

            var customer = new Customer(name, email);

            await _customerRepository.RegisterAsync(customer, passwordHash);

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

            var customerData = await _customerRepository.GetByEmailAsync(email);

            var passwordHashFromDb = customerData.passwordHash;

            var passwordHash = _passwordHasher.HashPassword(password);

            if (!Bcrypt.Verify(passwordHash, passwordHashFromDb))
                throw new AuthenticationException("The password doesn't match");

            return _jwtManager.GenerateToken(customerData.customer);
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
                throw new InvalidId("Id cannot be empty");

            await _customerRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during deleting user {UserId}", id);
            throw;
        }
    }
}