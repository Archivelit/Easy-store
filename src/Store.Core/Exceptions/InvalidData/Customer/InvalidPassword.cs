namespace Store.Core.Exceptions.InvalidData;

public class InvalidPassword : InvalidUserData
{
    public InvalidPassword(string message)
        : base(message)
    {
    }
    
    public InvalidPassword(string message, Exception inner)
        : base(message, inner)
    {
    }
}