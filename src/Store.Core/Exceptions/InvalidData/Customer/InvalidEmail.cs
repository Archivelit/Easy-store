namespace Store.Core.Exceptions.InvalidData;

public class InvalidEmail : InvalidUserData
{
    public InvalidEmail(string message)
        : base(message)
    {
    }
    
    public InvalidEmail(string message, Exception inner)
        : base(message, inner)
    {
    }
}