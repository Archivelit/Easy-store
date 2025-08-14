using Serilog;
using Store.App.GraphQl.Validation;
using Store.Core.Exceptions.InvalidData;

namespace Store.Core.Utils.Validators.Customer;

public class CustomerNameValidator : ICustomerNameValidator
{
    public bool ValidateCustomerName(string customerName)
    {
        if (string.IsNullOrEmpty(customerName))
            throw new InvalidName("Customer name cannot be null or empty.");

        if (!customerName.Any(char.IsLetter))
            throw new InvalidName("Invalid customer name. It must contain at least 1 letter.");

        if (customerName.Length < 3)
            throw new InvalidName("Name is too short. It has to have at least 3 characters.");

        if (customerName.Length > 100)
            throw new InvalidName("Name is too long. It has to be 100 characters.");

        Log.Debug("User name validated succesfuly");

        return true;
    }
}