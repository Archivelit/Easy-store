using Store.App.CQRS.Customers.Commands.Update.UpdateChain;
using Store.Core.Builders;
using Store.Core.Contracts.Repositories;
using Store.App.GraphQl.Security;
using Store.App.GraphQl.Validation;
using Store.Core.Models;
using Store.Core.Models.Dto.Customers;

namespace Store.App.CQRS.Customers.Commands.Update;

public class CustomerUpdateFacade
{
    private readonly ICustomerUpdateChain _chain;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordValidator _passwordValidator;
    private readonly IPasswordHasher _passwordHasher;

    public CustomerUpdateFacade(ICustomerUpdateChainFactory factory, IPasswordHasher passwordHasher, IPasswordValidator passwordValidator, ICustomerRepository customerRepository)
    {
        _passwordHasher = passwordHasher;
        _passwordValidator = passwordValidator;
        _customerRepository = customerRepository;
        _chain = factory.Create();
    }

    public async Task UpdateCustomerAsync(CustomerDto model, string password)
    {
        var customerData = await _customerRepository.GetByIdAsync(model.Id);

        var builder = new CustomerBuilder();
        builder.From(customerData.customer);

        var updateData = GetNewData(builder, model, password);

        if (string.IsNullOrEmpty(updateData.passwordHash))
        {
            updateData.passwordHash = customerData.passwordHash;
        }
        
        await _customerRepository.UpdateAsync(updateData.customer, updateData.passwordHash);
    }

    private (Customer customer, string passwordHash) GetNewData(CustomerBuilder builder, CustomerDto model, string password)
    {
        builder = _chain.Update(builder, model);
        var customer = builder.Build();
        
        string passwordHash;
        if (!string.IsNullOrWhiteSpace(password))
        {
            _passwordValidator.ValidatePassword(password);
            passwordHash = _passwordHasher.HashPassword(password);
        }
        else
        {
            passwordHash = string.Empty;
        }
        
        return (customer, passwordHash);
    }
}