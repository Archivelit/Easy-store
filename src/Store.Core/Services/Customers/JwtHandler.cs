using Store.Core.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using Path = System.IO.Path;
using Microsoft.Extensions.Logging;
using Store.Core.Contracts.Users;

namespace Store.Core.Services.Customers;

public class JwtHandler : IJwtManager
{
    private readonly SigningCredentials _signingCredentials;
    private readonly ILogger<JwtHandler> _logger;

    public JwtHandler(ILogger<JwtHandler> logger)
    {
        _signingCredentials = GetSigningCredentials();
        _logger = logger;
    }

    public string GenerateToken(User user)
    {
        _logger.LogDebug("Generating token for {UserId}", user.Id);

        var customerClaims = GenerateClaims(user);

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

        _logger.LogDebug("Token for {UserId} generetad", user.Id);

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

    private Claim[] GenerateClaims(User user) => [
                                                            new("Name", user.Name),
                                                            new("Email", user.Email),
                                                            new("Subscription", user.SubscriptionType.ToString()),
                                                            new("Id", user.Id.ToString())
                                                        ];
}