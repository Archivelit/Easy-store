using Store.Core.Models;

namespace Store.Core.Contracts.Customers;

public interface IJwtManager
{
    string GenerateToken(Customer customer);
    Task ValidateTokenAsync(string token);
}