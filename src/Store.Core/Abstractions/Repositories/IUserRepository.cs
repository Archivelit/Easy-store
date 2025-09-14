namespace Store.Core.Abstractions.Repositories;

public interface IUserRepository
{
    Task RegisterAsync(User user);
    Task DeleteAsync(Guid id);
    Task<bool> IsEmailClaimedAsync(string email);
    Task<bool> IsExistsAsync(Guid id);
    Task<User> GetByIdAsync(Guid id);
    Task UpdateAsync(User user);
}