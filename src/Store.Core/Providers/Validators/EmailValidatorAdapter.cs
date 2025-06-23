using Store.Core.Contracts.Validation;
using Store.API.Utils.Validators.Customer;

namespace Store.Core.Providers.Validators;

public class EmailValidatorAdapter : IEmailValidator
{
    public bool ValidateEmail(string email)
    {
        return EmailValidator.IsEmailValid(email);
    }
}