namespace Store.App.GraphQl.Security;

public interface IPasswordHasher
{
    string HashPassword(string password);
}