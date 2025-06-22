namespace Store.Core.Exceptions.InvalidData.Item;

public class InvalidItemTitle : InvalidItemData
{
    public InvalidItemTitle(string message)
        : base(message)
    {
    }
    
    public InvalidItemTitle(string message, Exception inner)
        : base(message, inner)
    {
    }
}