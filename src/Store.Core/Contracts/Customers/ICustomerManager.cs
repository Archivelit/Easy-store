using Store.Core.Models;

namespace Store.App.GraphQl.Customers;

public interface ICustomerManager
{
    Task<Customer> RegisterAsync(string name, string email, string password);
    Task<string> AuthenticateAsync(string email, string password);
    Task DeleteAsync(Guid id);
}