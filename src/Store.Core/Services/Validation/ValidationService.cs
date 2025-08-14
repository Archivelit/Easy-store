using Microsoft.Extensions.Logging;
using Store.App.GraphQl.Validation;

namespace Store.Core.Services.Validation;

public class ValidationService : ICustomerValidator
{
    private readonly IEmailValidator _emailValidator;
    private readonly ICustomerNameValidator _customerNameValidator;
    private readonly IPasswordValidator _passwordValidator;
    private readonly ISubscriptionValidator _subscriptionValidator;
    private readonly ILogger<ValidationService> _logger;

    public ValidationService(IEmailValidator emailValidator, ICustomerNameValidator customerNameValidator, IPasswordValidator passwordValidator, ISubscriptionValidator subscriptionValidator, ILogger<ValidationService> logger)
    {
        _emailValidator = emailValidator;
        _customerNameValidator = customerNameValidator;
        _passwordValidator = passwordValidator;
        _subscriptionValidator = subscriptionValidator;
        _logger = logger;
    }

    public void ValidateAndThrow(string name, string email, string password)
    {
        try
        {
            ValidateCustomerName(name);
            ValidateEmail(email);
            ValidatePassword(password);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "User data was not valid");
            throw;
        }
    }

    public void ValidateAndThrow(string name, string email, string password, string subscription)
    {
        ValidateAndThrow(name, email, password);
        try 
        {
            ValidateSubscription(subscription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "User data was not valid");
            throw;
        }
    }

    public void ValidateCustomerName(string customerName) =>
        _customerNameValidator.ValidateCustomerName(customerName);

    public void ValidateEmail(string email) =>
        _emailValidator.ValidateEmail(email);

    public void ValidatePassword(string password) =>
        _passwordValidator.ValidatePassword(password);
    
    public void ValidateSubscription(string subscription) =>
        _subscriptionValidator.ValidateSubscription(subscription);
}