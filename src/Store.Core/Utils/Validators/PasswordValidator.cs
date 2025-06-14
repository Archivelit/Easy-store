using Store.Core.Contracts.Validation;
using Store.Core.Exceptions.InvalidData;

namespace Store.Core.Utils.Validators;

public class PasswordValidator : IPasswordValidator
{
    public bool ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new InvalidPasswordException("Password cannot be empty.");

        if (password.Length < 8)
            throw new InvalidPasswordException("Password is too short.");

        if (!password.Any(char.IsLetter))
            throw new InvalidPasswordException("Password must contain at least one letter.");

        if (!password.Any(char.IsDigit))
            throw new InvalidPasswordException("Password must have at least one digit.");

        if (!password.Any(char.IsLower))
            throw new InvalidPasswordException("Password must contain at least one lower case letter.");

        if (!password.Any(char.IsUpper))
            throw new InvalidPasswordException("Password must contain at least one upper case letter.");

        if (!password.Any(IsSymbol))
            throw new InvalidPasswordException("Password must contain at least one symbol.");
        
        return true;
    }

    private bool IsSymbol(char character)
    {
        return !(char.IsLetterOrDigit(character) && char.IsWhiteSpace(character));
    }
}