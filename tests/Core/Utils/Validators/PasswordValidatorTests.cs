using Store.App.GraphQl.Validation;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Utils.Validators.User;

namespace Store.Tests.Core.Utils.Validators;

public class PasswordValidatorTests
{
    private readonly IPasswordValidator _passwordValidator = new PasswordValidator();

    [Fact]
    public void PasswordValidator_NullString()
    {
        string? password = null;
        
        Assert.Throws<InvalidPassword>(() => _passwordValidator.ValidatePassword(password));
    }

    [Fact]
    public void PasswordValidator_EmptyString()
    {
        string password = "";
        
        Assert.Throws<InvalidPassword>(() => _passwordValidator.ValidatePassword(password));
    }

    [Fact]
    public void PasswordValidator_WhitespaceString()
    {
        string password = new(' ', 10);
        
        Assert.Throws<InvalidPassword>(() => _passwordValidator.ValidatePassword(password));
    }

    [Fact]
    public void PasswordValidator_ValidPassword()
    {
        string password = "Pass1Word*";
        
        bool result = _passwordValidator.ValidatePassword(password);
        
        Assert.True(result);
    }

    [Fact]
    public void PasswordValidator_WithoutLetter()
    {
        string password = "1234567890*";
        
        Assert.Throws<InvalidPassword>(() => _passwordValidator.ValidatePassword(password));
    }

    [Fact]
    public void PasswordValidator_WithoutDigit()
    {
        string password = "ABCDEFgh*";
        
        Assert.Throws<InvalidPassword>(() => _passwordValidator.ValidatePassword(password));
    }

    [Fact]
    public void PasswordValidator_WithoutSpecial()
    {
        string password = "ABCD1234";
        
        Assert.Throws<InvalidPassword>(() => _passwordValidator.ValidatePassword(password));
    }

    [Fact]
    public void PasswordValidator_WithoutUpper()
    {
        string password = "abcd1234*";
        
        Assert.Throws<InvalidPassword>(() => _passwordValidator.ValidatePassword(password));
    }

    [Fact]
    public void PasswordValidator_WithoutLower()
    {
        string password = "ABCD1234*";

        Assert.Throws<InvalidPassword>(() => _passwordValidator.ValidatePassword(password));
    }

    [Fact]
    public void PasswordValidator_TooShort()
    {
        string password = "12Ab*";
        
        Assert.Throws<InvalidPassword>(() => _passwordValidator.ValidatePassword(password));
    }

    [Fact]
    public void PasswordValidator_MinLength()
    {
        string password = "ABc1234*";
        
        bool result = _passwordValidator.ValidatePassword(password);
        
        Assert.True(result);
    }

    [Fact]
    public void PasswordValidator_SupportsTilde_As_SpecialSymbol()
    {
        string password = "ABCd1234~";
        
        bool result = _passwordValidator.ValidatePassword(password);
        
        Assert.True(result);
    }
}