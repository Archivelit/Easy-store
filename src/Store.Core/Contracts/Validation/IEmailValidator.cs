namespace Store.Core.Contracts.Validation;

public interface IEmailValidator
{
    bool ValidateEmail(string email);
}