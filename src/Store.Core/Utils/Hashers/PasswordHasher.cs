using Store.Core.Contracts.Security;
using System.Security.Cryptography;
using System.Text;

namespace Store.Core.Utils.Hashers;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }
}