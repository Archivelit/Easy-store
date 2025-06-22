namespace Store.Core.Exceptions.InvalidData.Item;

public class InvalidItemPrice : InvalidItemData
{
    public InvalidItemPrice(string message)
        : base(message)
    {
    }
    
    public InvalidItemPrice(string message, Exception inner)
        : base(message, inner)
    {
    }
}