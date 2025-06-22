namespace Store.Core.Exceptions.InvalidData.Item;

public class InvalidItemDescription : InvalidItemData
{
    public InvalidItemDescription(string message)
        : base(message)
    {
    }
    
    public InvalidItemDescription(string message, Exception inner)
        : base(message, inner)
    {
    }
}