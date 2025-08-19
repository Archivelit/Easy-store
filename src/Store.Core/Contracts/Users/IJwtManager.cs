using Store.Core.Models;

namespace Store.Core.Contracts.Users;

public interface IJwtManager
{
    string GenerateToken(User user);
    Task ValidateTokenAsync(string token);
}