using Store.Core.Models;

namespace Store.Core.Contracts.Repositories;

public interface ICustomerRepository
{
    Task RegisterAsync(Customer customer, string passwordHash);
    Task<bool> IsEmailClaimed(string email);
    Task<(string email, string passwordHash)> GetCustomerPasswordHashByEmail(string email);
    Task<Customer> GetCustomerByEmail(string email);
}