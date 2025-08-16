using Store.Core.Models;

namespace Store.Core.Contracts.Repositories;

public interface IUserRepository
{
    Task RegisterAsync(User user, string passwordHash);
    Task DeleteAsync(Guid id);
    Task<bool> IsEmailClaimedAsync(string email);
    Task<(User user, string passwordHash)> GetByEmailAsync(string email);
    Task<bool> IsExistsAsync(Guid id);
    Task<(User user, string passwordHash)> GetByIdAsync(Guid id);
    Task UpdateAsync(User user, string passwordHash);
}