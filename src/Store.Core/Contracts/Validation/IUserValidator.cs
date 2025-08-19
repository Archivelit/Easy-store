namespace Store.Core.Contracts.Validation;

public interface IUserValidator
{
    void ValidateAndThrow(string name, string email, string password);
    void ValidateEmail(string email);
    void ValidatePassword(string password);
    void ValidateSubscription(string subscription);
    void ValidateCustomerName(string name);
}