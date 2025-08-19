using Serilog;
using Store.Core.Contracts.Validation;
using Store.Core.Exceptions.InvalidData;

namespace Store.Core.Utils.Validators.User;

public class UserNameValidator : IUserNameValidator
{
    public bool ValidateUserName(string userName)
    {
        if (string.IsNullOrEmpty(userName))
            throw new InvalidUserDataException("User name cannot be null or empty.");

        if (!userName.Any(char.IsLetter))
            throw new InvalidUserDataException("Invalid user name. It must contain at least 1 letter.");

        if (userName.Length < 3)
            throw new InvalidUserDataException("Name is too short. It has to have at least 3 characters.");

        if (userName.Length > 100)
            throw new InvalidUserDataException("Name is too long. It has to be 100 characters.");

        Log.Debug("User name validated succesfuly");

        return true;
    }
}