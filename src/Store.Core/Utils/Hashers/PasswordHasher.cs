using Store.App.GraphQl.Security;
using Bcrypt = BCrypt.Net.BCrypt;

namespace Store.Core.Utils.Hashers;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return Bcrypt.HashPassword(password);
    }
}