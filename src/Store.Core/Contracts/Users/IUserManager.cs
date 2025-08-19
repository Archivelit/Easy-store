using Store.Core.Models;

namespace Store.Core.Contracts.Users;

public interface IUserManager
{
    Task<User> RegisterAsync(string name, string email, string password);
    Task<string> AuthenticateAsync(string email, string password);
    Task DeleteAsync(Guid id);
}