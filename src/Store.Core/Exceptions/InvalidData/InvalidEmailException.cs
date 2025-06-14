namespace Store.Core.Exceptions.InvalidData;

public class InvalidEmailException : InvalidUserDataException
{
    public InvalidEmailException(string message)
        : base(message)
    {
    }
    
    public InvalidEmailException(string message, Exception inner)
        : base(message, inner)
    {
    }
}