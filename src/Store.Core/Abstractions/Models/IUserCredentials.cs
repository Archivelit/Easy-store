namespace Store.Core.Abstractions.Models;

public interface IUserCredentials
{
    public Guid Id { get; }
    public Role Role { get; }
    public string PasswordHash { get; }
}
