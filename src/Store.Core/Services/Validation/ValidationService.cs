using Store.App.GraphQl.Validation;

namespace Store.Core.Services.Validation;

public class ValidationService : ICustomerValidator
{
    private readonly IEmailValidator _emailValidator;
    private readonly ICustomerNameValidator _customerNameValidator;
    private readonly IPasswordValidator _passwordValidator;
    private readonly ISubscriptionValidator _subscriptionValidator;
    
    public ValidationService(IEmailValidator emailValidator, ICustomerNameValidator customerNameValidator, IPasswordValidator passwordValidator, ISubscriptionValidator subscriptionValidator)
    {
        _emailValidator = emailValidator;
        _customerNameValidator = customerNameValidator;
        _passwordValidator = passwordValidator;
        _subscriptionValidator = subscriptionValidator;
    }

    public void ValidateAndThrow(string name, string email, string password)
    {
        ValidateCustomerName(name);
        ValidateEmail(email);
        ValidatePassword(password);
    }
    
    public void ValidateAndThrow(string name, string email, string password, string subscription)
    {
        ValidateCustomerName(name);
        ValidateEmail(email);
        ValidatePassword(password);
        ValidateSubscription(subscription);
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