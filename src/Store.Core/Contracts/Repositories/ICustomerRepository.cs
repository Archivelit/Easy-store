using Store.Core.Models;

namespace Store.Core.Contracts.Repositories;

public interface ICustomerRepository
{
    Task RegisterAsync(Customer customer, string passwordHash);
    Task DeleteAsync(Guid id);
    Task<bool> IsEmailClaimed(string email);
    Task<string> GetPasswordHashByEmail(string email);
    Task<Customer> GetCustomerByEmail(string email);
}