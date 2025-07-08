using Store.Core.Models;

namespace Store.Core.Contracts.Repositories;

public interface ICustomerRepository
{
    Task RegisterAsync(Customer customer, string passwordHash);
    Task DeleteAsync(Guid id);
    Task<bool> IsEmailClaimedAsync(string email);
    Task<(Customer customer, string passwordHash)> GetByEmailAsync(string email);
    Task<bool> IsExistsAsync(Guid id);
    Task<(Customer customer, string passwordHash)> GetByIdAsync(Guid id);
    Task UpdateAsync(Customer customer, string passwordHash);
}