using Store.Core.Models;

namespace Store.Core.Contracts.Repositories;

public interface ICustomerRepository
{
    Task RegisterAsync(Customer customer, string passwordHash);
    Task DeleteAsync(Guid id);
    Task<bool> IsEmailClaimed(string email);
    Task<(Customer customer, string passwordHash)> GetCustomerDataByEmail(string email);
    Task<bool> IsCustomerExistsAsync(Guid id);
}