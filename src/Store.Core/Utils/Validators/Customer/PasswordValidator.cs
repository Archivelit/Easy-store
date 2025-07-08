using Store.App.GraphQl.Validation;
using Store.Core.Exceptions.InvalidData;

namespace Store.Core.Utils.Validators.Customer;

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

    private bool IsSymbol(char c) =>
        !(char.IsLetterOrDigit(c) && char.IsWhiteSpace(c));
}