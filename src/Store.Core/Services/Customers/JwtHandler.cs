using Store.Core.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Store.Core.Contracts.Customers;
using System.Security.Authentication;

namespace Store.Core.Services.Customers;

public class JwtHandler : IJwtManager
{
    private readonly SigningCredentials _signingCredentials;

    public JwtHandler() =>
        _signingCredentials = GetSigningCredentials();
    
    public string GenerateToken(Customer customer)
    {
        var customerClaims = GenerateClaims(customer);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(customerClaims),
            Expires = DateTime.Now.AddMinutes(15),
            SigningCredentials = _signingCredentials,
            Issuer = "localhost",
            Audience = "myapp"
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }

    public async Task ValidateTokenAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = _signingCredentials.Key,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(10),
            ValidIssuer = "localhost",
            ValidAudience = "myapp"
        };

        var result = await tokenHandler.ValidateTokenAsync(token, validationParameters);

        if (!result.IsValid)
            throw new AuthenticationException("The token is invalid");
    }
        
    private SigningCredentials GetSigningCredentials()
    {
        var keyText = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "secret.key"));
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(keyText));
        return new (securityKey, SecurityAlgorithms.HmacSha256);
    }

    private Claim[] GenerateClaims(Customer customer) => [
                                                            new("Name", customer.Name),
                                                            new("Email", customer.Email),
                                                            new("Subscription", customer.SubscriptionType.ToString()),
                                                            new("Id", customer.Id.ToString())
                                                        ];
}