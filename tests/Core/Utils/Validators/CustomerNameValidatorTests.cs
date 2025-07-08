using Store.App.GraphQl.Validation;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Utils.Validators.Customer;

namespace Store.Tests.Core.Utils.Validators;

#nullable enable

public class CustomerNameValidatorTests
{
    private readonly ICustomerNameValidator _customerNameValidator = new CustomerNameValidator();

    [Fact]
    public void CustomerNameValidator_MinLength()
    {
        string name = "abc";
        
        bool result = _customerNameValidator.ValidateCustomerName(name);
        
        Assert.True(result);
    }

    [Fact]
    public void CustomerNameValidator_MaxLength()
    {
        string name = new string('a', 100);
        
        bool result = _customerNameValidator.ValidateCustomerName(name);
        
        Assert.True(result);
    }

    [Fact]
    public void CustomerNameValidator_TooShort()
    {
        string name = "aa";
        
        Assert.Throws<InvalidName>(() => _customerNameValidator.ValidateCustomerName(name));
    }
    
    [Fact]
    public void CustomerNameValidator_TooLong()
    {
        string name = new string('a', 101);
        
        Assert.Throws<InvalidName>(() => _customerNameValidator.ValidateCustomerName(name));
    }

    [Fact]
    public void CustomerNameValidator_WithoutLetters()
    {
        string name = "123";
        
        Assert.Throws<InvalidName>(() => _customerNameValidator.ValidateCustomerName(name));
    }

    [Fact]
    public void CustomerNameValidator_OneLetter()
    {
        string name = "a12";
        
        bool result = _customerNameValidator.ValidateCustomerName(name);
        
        Assert.True(result);
    }

    [Fact]
    public void CustomerNameValidator_NullString()
    {
        string name = null;

        Assert.Throws<InvalidName>(() => _customerNameValidator.ValidateCustomerName(name));
    }

    [Fact]
    public void CustomerNameValidator_EmptyString()
    {
        string name = "";
        
        Assert.Throws<InvalidName>(() => _customerNameValidator.ValidateCustomerName(name));
    }

    [Fact]
    public void CustomerNameValidator_WhiteSpaceString()
    {
        string name = "   ";
        
        Assert.Throws<InvalidName>(() => _customerNameValidator.ValidateCustomerName(name));
    }
}