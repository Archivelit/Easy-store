using Store.Core.Contracts.Security;
using System.Security.Authentication;
using Bcrypt = BCrypt.Net.BCrypt;

namespace Store.Core.Utils.Hashers;

public class PasswordManager : IPasswordManager
{
    public string HashPassword(string password)
    {
        return Bcrypt.HashPassword(password);
    }

    public void VerifyPassword(string hash, string password)
    {
        if (!Bcrypt.Verify(password, hash))
            throw new AuthenticationException("The password doesn't match");
    }
}