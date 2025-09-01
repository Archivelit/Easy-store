using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Contracts.Dao;

public interface IUserDao
{
    Task RegisterAsync(UserEntity entity);
    Task<bool> IsEmailClaimedAsync(string email);
    Task<UserEntity?> GetByEmailAsync(string email);
    Task DeleteAsync(UserEntity entity);
    Task<bool> IsExistsAsync(Guid id);
    Task<UserEntity?> GetByIdAsync(Guid id);
    Task UpdateAsync(UserEntity entity);
}