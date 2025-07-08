using System.Security.Authentication;
using Store.App.GraphQl.Customers;
using Store.Core.Contracts.Repositories;
using Store.App.GraphQl.Security;
using Store.App.GraphQl.Validation;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Models;
using Bcrypt = BCrypt.Net.BCrypt;

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

    public async Task<Customer> RegisterAsync(string name, string email, string password)
    {
        _validationService.ValidateAndThrow(name, email, password);
        
        if(await _customerRepository.IsEmailClaimedAsync(email))
            throw new InvalidEmail("Email is already registered");
        
        var passwordHash = _passwordHasher.HashPassword(password);

        var customer = new Customer(name, email);
        
        await _customerRepository.RegisterAsync(customer, passwordHash);

        return customer;
    }

    public async Task<string> AuthenticateAsync(string email, string password)
    {
        _validationService.ValidateEmail(email);
        _validationService.ValidatePassword(password);
        
        var customerData = await _customerRepository.GetByEmailAsync(email);

        var passwordHashFromDb = customerData.passwordHash;
        
        var passwordHash = _passwordHasher.HashPassword(password);
        
        if (!Bcrypt.Verify(passwordHash, passwordHashFromDb))
            throw new AuthenticationException("The password doesn't match");
        
        return _jwtManager.GenerateToken(customerData.customer);
    }

    public async Task DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new InvalidId("Id cannot be empty");
        
        await _customerRepository.DeleteAsync(id);
    }
}