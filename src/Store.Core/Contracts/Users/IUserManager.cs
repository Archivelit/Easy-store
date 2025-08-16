using Store.Core.Models;

namespace Store.App.GraphQl.Users;

public interface IUserManager
{
    Task<User> RegisterAsync(string name, string email, string password);
    Task<string> AuthenticateAsync(string email, string password);
    Task DeleteAsync(Guid id);
}