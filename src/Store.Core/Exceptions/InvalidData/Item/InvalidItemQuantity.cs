namespace Store.Core.Exceptions.InvalidData.Item;

public class InvalidItemQuantity : InvalidItemData
{
    public InvalidItemQuantity(string message)
        : base(message)
    {
    }
    
    public InvalidItemQuantity(string message, Exception inner)
        : base(message, inner)
    {
    }
}