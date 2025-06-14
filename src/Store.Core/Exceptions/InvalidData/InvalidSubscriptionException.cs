namespace Store.Core.Exceptions.InvalidData;

public class InvalidSubscriptionException : InvalidUserDataException
{
    public InvalidSubscriptionException(string message)
        : base(message)
    {
    }
    
    public InvalidSubscriptionException(string message, Exception inner)
        : base(message, inner)
    {
    }
}