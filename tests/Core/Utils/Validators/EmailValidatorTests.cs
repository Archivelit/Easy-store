using System.Text;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Providers.Validators;

namespace Store.Tests.Core.Utils.Validators;

public class EmailValidatorTests
{
    private readonly EmailValidatorAdapter _emailValidator = new();

    [Fact]
    public void EmailValidator_ValidEmail()
    {
        string email = "validEmail@example.com";

        bool result = _emailValidator.ValidateEmail(email);

        Assert.True(result);
    }

    [Fact]
    public void EmailValidator_WithoutAtSign()
    {
        string email = "invalidEmailExample.com";

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }

    [Fact]
    public void EmailValidator_TwoAtSigns()
    {
        string email = "invalid@Email@Expample.com";

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }

    [Fact]
    public void EmailValidator_WhiteSpaceString()
    {
        string email = " ";

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }

    [Fact]
    public void EmailValidator_NullString()
    {
        string? email = null;

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }

    [Fact]
    public void EmailValidator_EmptyString()
    {
        string email = "";

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }

    [Fact]
    public void EmailValidator_ContainsWhiteSpace()
    {
        string email = "invalid Email@Expample.com";

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }

    [Fact]
    public void EmailValidator_TooLongEmail()
    {
        var sb = new StringBuilder();

        sb.Append('a', 65);
        sb.Append('@');
        sb.Append("example.com");
        
        var email = sb.ToString();

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }

    [Fact]
    public void EmailValidator_MaxLength()
    {
        var sb = new StringBuilder();

        sb.Append('a', 64);
        sb.Append('@');
        sb.Append('a', 185);
        sb.Append(".com");

        bool result = _emailValidator.ValidateEmail(sb.ToString());

        Assert.True(result);
    }

    [Fact]
    public void EmailValidator_WithDotOnEnd()
    {
        string email = "a@b.";

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }

    [Fact]
    public void EmailValidator_WithoutLocalName()
    {
        string email = "@example.com";

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }

    [Fact]
    public void EmailValidator_SpecialSymbols_LocalName()
    {
        string email = "invalid!email@example.com";

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }
    
    [Fact]
    public void EmailValidator_WithUnderscore_LocalName()
    {
        string email = "invalid_email@example.com";

        bool result = _emailValidator.ValidateEmail(email);

        Assert.True(result);
    }
    
    [Fact]
    public void EmailValidator_WithUnderscore_Domain()
    {
        string email = "invalid_email@example_domain.com";

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }
    
    [Fact]
    public void EmailValidator_MultiLevelDomain()
    {
        string email = "email@example.domain.com";

        bool result = _emailValidator.ValidateEmail(email);

        Assert.True(result);
    }

    [Fact]
    public void EmailValidator_NumbersInDomain()
    {
        string email = "email@example123.com";

        bool result = _emailValidator.ValidateEmail(email);

        Assert.True(result);
    }

    [Fact]
    public void EmailValidator_ContainsDot_LocalPart()
    {
        string email = "user.email@example.com";

        bool result = _emailValidator.ValidateEmail(email);

        Assert.True(result);
    }

    [Fact]
    public void EmailValidator_ContainsPlus_LocalPart()
    {
        string email = "user+email@example.com";

        bool result = _emailValidator.ValidateEmail(email);

        Assert.True(result);
    }

    [Fact]
    public void EmailValidator_CaseInsensitive()
    {
        string email1 = "email@example.com";
        string email2 = "EMAIL@EXAMPLE.COM";
        
        bool result1 = _emailValidator.ValidateEmail(email1);
        bool result2 = _emailValidator.ValidateEmail(email2);
        
        Assert.Equal(result1, result2);
    }

    [Fact]
    public void EmailValidator_DotOnStart_LocalName()
    {
        string email = ".email@example.com";
        
        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }

    [Fact]
    public void EmailValidator_DotOnEnd_LocalName()
    {
        string email = "email.@example.com";
        
        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }

    [Fact]
    public void EmailValidator_TwoDotsInRow()
    {
        string email = "example..email@example.com";

        Assert.Throws<InvalidEmailException>(() => _emailValidator.ValidateEmail(email));
    }
}