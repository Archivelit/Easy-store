namespace Store.Core.Exceptions.InvalidData;

public class InvalidId : InvalidUserData
{
    public InvalidId(string message)
        : base(message)
    {
    }
    
    public InvalidId(string message, Exception inner)
        : base(message, inner)
    {
    }
}