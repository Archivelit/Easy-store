namespace Store.Core.Exceptions.InvalidData;

public class InvalidIdException : InvalidUserDataException
{
    public InvalidIdException(string message)
        : base(message)
    {
    }
    
    public InvalidIdException(string message, Exception inner)
        : base(message, inner)
    {
    }
}