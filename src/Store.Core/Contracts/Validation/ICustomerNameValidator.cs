namespace Store.Core.Contracts.Validation;

public interface ICustomerNameValidator
{
    bool ValidateCustomerName(string customerName);
}