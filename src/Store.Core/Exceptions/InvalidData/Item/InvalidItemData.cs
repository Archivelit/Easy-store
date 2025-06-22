namespace Store.Core.Exceptions.InvalidData.Item;

public class InvalidItemData : Exception
{
    public InvalidItemData(string message)
        : base(message)
    {
    }
    
    public InvalidItemData(string message, Exception inner)
        : base(message, inner)
    {
    }
}