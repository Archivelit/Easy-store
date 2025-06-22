namespace Store.Core.Exceptions.InvalidData;

public class InvalidUserData : Exception
{
    public InvalidUserData(string message)
        : base(message)
    {
    }
    
    public InvalidUserData(string message, Exception inner)
        : base(message, inner)
    {
    }
}