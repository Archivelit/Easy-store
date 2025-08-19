using Store.Core.Contracts.Validation;
using Store.Core.Utils.Validators.User;

namespace Store.Core.Providers.Validators;

public class EmailValidatorAdapter : IEmailValidator
{
    public bool ValidateEmail(string email)  => EmailValidator.IsEmailValid(email);
}