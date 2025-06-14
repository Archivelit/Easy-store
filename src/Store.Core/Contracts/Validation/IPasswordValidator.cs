namespace Store.Core.Contracts.Validation;

public interface IPasswordValidator
{
    bool ValidatePassword(string password);
}