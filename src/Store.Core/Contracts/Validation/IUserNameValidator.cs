namespace Store.App.GraphQl.Validation;

public interface IUserNameValidator
{
    bool ValidateCustomerName(string customerName);
}