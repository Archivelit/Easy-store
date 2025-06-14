using System.Security.Authentication;
using Store.Core.Contracts.Customers;
using Store.Core.Contracts.Repositories;
using Store.Core.Contracts.Security;
using Store.Core.Contracts.Validation;
using Store.Core.Exceptions.InvalidData;

namespace Store.Core.Managers;

public class CustomersManager : ICustomerManager
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerValidator _validationService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtManager _jwtManager;
    
    public CustomersManager(ICustomerRepository customerRepository, ICustomerValidator validationService, 
        IPasswordHasher passwordHasher, IJwtManager jwtManager)
    {
        _customerRepository = customerRepository;
        _validationService = validationService;
        _passwordHasher = passwordHasher;
        _jwtManager = jwtManager;
    }

    public async Task RegisterAsync(string name, string email, string password)
    {
        _validationService.ValidateAndThrow(name, email, password);
        
        if(await _customerRepository.IsEmailClaimed(email))
            throw new InvalidEmailException("Email is already registered");
        
        var passwordHash = _passwordHasher.HashPassword(password);
        
        await _customerRepository.RegisterAsync(new (name, email), passwordHash);
    }

    public async Task<string> AuthenticateAsync(string email, string password)
    {
        _validationService.ValidateEmail(email);
        _validationService.ValidatePassword(password);
        
        var customerData = await _customerRepository.GetCustomerPasswordHashByEmail(email);
        
        var passwordHash = _passwordHasher.HashPassword(password);
        
        if (customerData.passwordHash != passwordHash)
            throw new AuthenticationException("The password doesn't match");
        
        return _jwtManager.GenerateToken( await _customerRepository.GetCustomerByEmail(email));
    }
}