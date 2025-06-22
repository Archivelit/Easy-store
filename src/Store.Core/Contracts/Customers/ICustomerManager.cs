using Store.Core.Models;

namespace Store.Core.Contracts.Customers;

public interface ICustomerManager
{
    Task RegisterAsync(string name, string email, string password);
    Task<string> AuthenticateAsync(string email, string password);
    Task DeleteAsync(Guid id);
}