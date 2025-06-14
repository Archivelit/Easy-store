namespace Store.Core.Exceptions.InvalidData;

public class InvalidNameException : InvalidUserDataException
{
    public InvalidNameException(string message)
        : base(message)
    {
    }
    
    public InvalidNameException(string message, Exception inner)
        : base(message, inner)
    {
    }
}