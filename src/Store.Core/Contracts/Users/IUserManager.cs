using Store.Core.Models;

namespace Store.Core.Contracts.Users;

public interface IUserManager
{
    Task<User> RegisterAsync(string name, string email);
    Task DeleteAsync(Guid id);
}