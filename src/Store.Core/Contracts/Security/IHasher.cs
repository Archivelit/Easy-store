namespace Store.Core.Contracts.Security;

public interface IPasswordHasher
{
    string HashPassword(string password);
}