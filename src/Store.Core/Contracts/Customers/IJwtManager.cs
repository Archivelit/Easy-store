using Store.Core.Models;

namespace Store.App.GraphQl.Customers;

public interface IJwtManager
{
    string GenerateToken(Customer customer);
    Task ValidateTokenAsync(string token);
}