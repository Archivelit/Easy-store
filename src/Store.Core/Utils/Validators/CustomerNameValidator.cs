using Store.Core.Contracts.Validation;
using Store.Core.Exceptions.InvalidData;

namespace Store.Core.Utils.Validators;

public class CustomerNameValidator : ICustomerNameValidator
{
    public bool ValidateCustomerName(string customerName)
    {
        if (string.IsNullOrEmpty(customerName))
            throw new InvalidNameException("Customer name cannot be null or empty.");

        if (!customerName.Any(char.IsLetter))
            throw new InvalidNameException("Invalid customer name. It must contain at least 1 letter.");

        if (customerName.Length < 3)
            throw new InvalidNameException("Name is too short. It has to have at least 3 characters.");

        if (customerName.Length > 100)
            throw new InvalidNameException("Name is too long. It has to be 100 characters.");
        
        return true;
    }
}