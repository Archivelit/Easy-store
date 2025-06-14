namespace Store.Core.Exceptions.InvalidData;

public class InvalidUserDataException : Exception
{
    public InvalidUserDataException(string message)
        : base(message)
    {
    }
    
    public InvalidUserDataException(string message, Exception inner)
        : base(message, inner)
    {
    }
}