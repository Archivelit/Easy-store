namespace Store.App.GraphQl.Validation;

public interface ICustomerNameValidator
{
    bool ValidateCustomerName(string customerName);
}