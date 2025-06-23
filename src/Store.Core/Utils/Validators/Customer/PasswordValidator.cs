using Store.Core.Contracts.Validation;
using Store.Core.Exceptions.InvalidData;

namespace Store.API.Utils.Validators.Customer;

public class PasswordValidator : IPasswordValidator
{
    public bool ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new InvalidPassword("Password cannot be empty.");

        if (password.Length < 8)
            throw new InvalidPassword("Password is too short.");

        if (!password.Any(char.IsLetter))
            throw new InvalidPassword("Password must contain at least one letter.");

        if (!password.Any(char.IsDigit))
            throw new InvalidPassword("Password must have at least one digit.");

        if (!password.Any(char.IsLower))
            throw new InvalidPassword("Password must contain at least one lower case letter.");

        if (!password.Any(char.IsUpper))
            throw new InvalidPassword("Password must contain at least one upper case letter.");

        if (!password.Any(IsSymbol))
            throw new InvalidPassword("Password must contain at least one symbol.");
        
        return true;
    }

    private bool IsSymbol(char character)
    {
        return !(char.IsLetterOrDigit(character) && char.IsWhiteSpace(character));
    }
}