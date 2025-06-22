using System.Text.RegularExpressions;
using Store.Core.Exceptions.InvalidData;

namespace Store.Core.Utils.Validators;

internal static class EmailValidator
{
    private static readonly Regex _emailRegex = new(@"^[\w\-\+]+(\.(?!\.)[\w\-\+]+)*@([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,}$" , RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly int MaxEmailLength = 254;
    private static readonly int MaxDomainLength = 189;
    
    public static bool IsEmailValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidEmail("Email cannot be null or empty.");

        var localPartLength = email.IndexOf("@", StringComparison.Ordinal);
        
        switch (localPartLength)
        {
            case -1:
                throw new InvalidEmail("Invalid email format.");
                
            case > 64:  // 64 - Max local length
                throw new InvalidEmail("Max local length exceeded. It has to be 64 characters.");
        }

        if (email.Length - localPartLength - 1 > MaxDomainLength)
            throw new InvalidEmail("Max domain length exceeded. It has to be 189 characters.");
        
        if (email.Length > MaxEmailLength)
            throw new InvalidEmail("Email is too long.");
        
        if (!_emailRegex.IsMatch(email)) 
            throw new InvalidEmail("Invalid email.");

        return true;
    }
}