namespace Store.Core.Exceptions.InvalidData;

public class InvalidSubscription : InvalidUserData
{
    public InvalidSubscription(string message)
        : base(message)
    {
    }
    
    public InvalidSubscription(string message, Exception inner)
        : base(message, inner)
    {
    }
}