namespace Store.Core.Exceptions.InvalidData;

public class InvalidPasswordException : InvalidUserDataException
{
    public InvalidPasswordException(string message)
        : base(message)
    {
    }
    
    public InvalidPasswordException(string message, Exception inner)
        : base(message, inner)
    {
    }
}