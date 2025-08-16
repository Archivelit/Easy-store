using Store.Core.Models;

namespace Store.App.GraphQl.Users;

public interface IJwtManager
{
    string GenerateToken(User user);
    Task ValidateTokenAsync(string token);
}