using Store.App.CQRS.Customers.Commands.Update.UpdateChain;
using Store.Core.Builders;
using Store.Core.Contracts.Repositories;
using Store.App.GraphQl.Security;
using Store.App.GraphQl.Validation;
using Store.Core.Models;
using Store.Core.Models.Dto.Customers;
using Microsoft.Extensions.Logging;

namespace Store.App.CQRS.Customers.Commands.Update;

public class CustomerUpdateFacade
{
    private readonly ICustomerUpdateChain _chain;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordValidator _passwordValidator;
    private readonly IPasswordHasher _passwordHasher;

    private readonly ILogger<CustomerUpdateFacade> _logger;

    public CustomerUpdateFacade(ICustomerUpdateChainFactory factory, IPasswordHasher passwordHasher, IPasswordValidator passwordValidator, ICustomerRepository customerRepository, ILogger<CustomerUpdateFacade> logger)
    {
        _passwordHasher = passwordHasher;
        _passwordValidator = passwordValidator;
        _customerRepository = customerRepository;
        _chain = factory.Create();
        _logger = logger;
    }

    public async Task UpdateCustomerAsync(CustomerDto model, string password)
    {
        _logger.LogDebug("Updating user in {method}", nameof(UpdateCustomerAsync));

        var customerData = await _customerRepository.GetByIdAsync(model.Id);

        var builder = new CustomerBuilder();
        builder.From(customerData.customer);

        var updateData = GetNewData(builder, model, password);

        if (string.IsNullOrEmpty(updateData.passwordHash))
        {
            updateData.passwordHash = customerData.passwordHash;
        }
        
        await _customerRepository.UpdateAsync(updateData.customer, updateData.passwordHash);

        _logger.LogDebug("End updating user");
    }

    private (Customer customer, string passwordHash) GetNewData(CustomerBuilder builder, CustomerDto model, string password)
    {
        _logger.LogDebug("Trying to extract data for update");
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

        _logger.LogDebug("Data extracted succesfuly");

        return (customer, passwordHash);
    }
}