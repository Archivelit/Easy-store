namespace Store.Core.Exceptions.InvalidData;

public class InvalidName : InvalidUserData
{
    public InvalidName(string message)
        : base(message)
    {
    }
    
    public InvalidName(string message, Exception inner)
        : base(message, inner)
    {
    }
}