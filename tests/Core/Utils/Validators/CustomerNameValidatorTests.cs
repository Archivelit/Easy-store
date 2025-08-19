using Store.Core.Contracts.Validation;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Utils.Validators.User;

namespace Store.Tests.Core.Utils.Validators;

#nullable enable

public class CustomerNameValidatorTests
{
    private readonly IUserNameValidator _customerNameValidator = new UserNameValidator();

    [Fact]
    public void CustomerNameValidator_MinLength()
    {
        string name = "abc";
        
        bool result = _customerNameValidator.ValidateUserName(name);
        
        Assert.True(result);
    }

    [Fact]
    public void CustomerNameValidator_MaxLength()
    {
        string name = new string('a', 100);
        
        bool result = _customerNameValidator.ValidateUserName(name);
        
        Assert.True(result);
    }

    [Fact]
    public void CustomerNameValidator_TooShort()
    {
        string name = "aa";
        
        Assert.Throws<InvalidUserDataException>(() => _customerNameValidator.ValidateUserName(name));
    }
    
    [Fact]
    public void CustomerNameValidator_TooLong()
    {
        string name = new string('a', 101);
        
        Assert.Throws<InvalidUserDataException>(() => _customerNameValidator.ValidateUserName(name));
    }

    [Fact]
    public void CustomerNameValidator_WithoutLetters()
    {
        string name = "123";
        
        Assert.Throws<InvalidUserDataException>(() => _customerNameValidator.ValidateUserName(name));
    }

    [Fact]
    public void CustomerNameValidator_OneLetter()
    {
        string name = "a12";
        
        bool result = _customerNameValidator.ValidateUserName(name);
        
        Assert.True(result);
    }

    [Fact]
    public void CustomerNameValidator_NullString()
    {
        string name = null;

        Assert.Throws<InvalidUserDataException>(() => _customerNameValidator.ValidateUserName(name));
    }

    [Fact]
    public void CustomerNameValidator_EmptyString()
    {
        string name = "";
        
        Assert.Throws<InvalidUserDataException>(() => _customerNameValidator.ValidateUserName(name));
    }

    [Fact]
    public void CustomerNameValidator_WhiteSpaceString()
    {
        string name = "   ";
        
        Assert.Throws<InvalidUserDataException>(() => _customerNameValidator.ValidateUserName(name));
    }
}