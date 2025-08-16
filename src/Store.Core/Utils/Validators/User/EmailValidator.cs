using Serilog;
using Store.Core.Exceptions.InvalidData;
using System.Text.RegularExpressions;

namespace Store.Core.Utils.Validators.User;

internal static class EmailValidator
{
    private static readonly Regex _emailRegex = new(@"^[\w\-\+]+(\.(?!\.)[\w\-\+]+)*@([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,}$" , RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly int MaxEmailLength = 254;
    private static readonly int MaxDomainLength = 189;
    
    public static bool IsEmailValid(string email)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(email);

        var localPartLength = email.IndexOf("@", StringComparison.Ordinal);
        
        switch (localPartLength)
        {
            case -1:
                throw new InvalidUserDataException("Invalid email format.");
                
            case > 64:  // 64 - Max local length
                throw new InvalidUserDataException("Max local length exceeded. It has to be 64 characters.");
        }

        if (email.Length - localPartLength - 1 > MaxDomainLength)
            throw new InvalidUserDataException("Max domain length exceeded. It has to be 189 characters.");
        
        if (email.Length > MaxEmailLength)
            throw new InvalidUserDataException("Email is too long.");
        
        if (!_emailRegex.IsMatch(email)) 
            throw new InvalidUserDataException("Invalid email.");

        Log.Debug("User email validated succesfuly");

        return true;
    }
}