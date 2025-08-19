using Serilog;
using Store.Core.Contracts.Validation;
using Store.Core.Exceptions.InvalidData;

namespace Store.Core.Utils.Validators.User;

public class PasswordValidator : IPasswordValidator
{
    public bool ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new InvalidUserDataException("Password cannot be empty.");

        if (password.Length < 8)
            throw new InvalidUserDataException("Password is too short.");

        if (!password.Any(char.IsLetter))
            throw new InvalidUserDataException("Password must contain at least one letter.");

        if (!password.Any(char.IsDigit))
            throw new InvalidUserDataException("Password must have at least one digit.");

        if (!password.Any(char.IsLower))
            throw new InvalidUserDataException("Password must contain at least one lower case letter.");

        if (!password.Any(char.IsUpper))
            throw new InvalidUserDataException("Password must contain at least one upper case letter.");

        if (!password.Any(IsSymbol))
            throw new InvalidUserDataException("Password must contain at least one symbol.");

        Log.Debug("User password validated succesfuly");

        return true;
    }

    private bool IsSymbol(char c) =>
        !(char.IsLetterOrDigit(c) && char.IsWhiteSpace(c));
}