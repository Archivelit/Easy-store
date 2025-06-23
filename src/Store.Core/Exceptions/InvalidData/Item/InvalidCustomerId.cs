namespace Store.Core.Exceptions.InvalidData.Item;

public class InvalidCustomerId : InvalidItemData
{
    public InvalidCustomerId(string message)
        : base(message)
    {
    }
    
    public InvalidCustomerId(string message, Exception inner)
        : base(message, inner)
    {
    }
}