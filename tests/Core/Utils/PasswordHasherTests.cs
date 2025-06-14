using Store.Core.Utils.Hashers;
using Store.Core.Contracts.Security;

namespace Store.Tests.Core.Utils;
public class PasswordHasherTests
{
    private readonly IPasswordHasher _hasher = new PasswordHasher();

    [Fact]
    public void HashPassword_SameInput_ReturnsSameHash()
    {
        var password = "MySecret123";
        
        var hash1 = _hasher.HashPassword(password);
        var hash2 = _hasher.HashPassword(password);
        
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void HashPassword_DifferentInput_ReturnsDifferentHash()
    {
        var password1 = "Password1";
        var password2 = "Password2";
        
        var hash1 = _hasher.HashPassword(password1);
        var hash2 = _hasher.HashPassword(password2);
        
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void HashPassword_KnownInput_ReturnsExpectedHash()
    {
        var password = "test";
        var expectedHash = "9F86D081884C7D659A2FEAA0C55AD015A3BF4F1B2B0B822CD15D6C15B0F00A08";
        
        var actualHash = _hasher.HashPassword(password);
        
        Assert.Equal(expectedHash, actualHash);
    }
}