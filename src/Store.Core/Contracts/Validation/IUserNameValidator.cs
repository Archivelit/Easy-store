namespace Store.Core.Contracts.Validation;

public interface IUserNameValidator
{
    bool ValidateUserName(string customerName);
}