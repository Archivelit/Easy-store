namespace Store.Core.Abstractions;

public interface IPasswordHasher
{
    public string Hash(string password);
}
