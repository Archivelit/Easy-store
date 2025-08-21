namespace Store.Core.Contracts.Security;

public interface IPasswordManager
{
    string HashPassword(string password);
    void VerifyPassword(string hash, string password);
}