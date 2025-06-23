namespace Store.Core.Exceptions.InvalidData.Item;

public class InvalidItemCreateTime : InvalidItemData
{
    public InvalidItemCreateTime(string message)
        : base(message)
    {
    }
    
    public InvalidItemCreateTime(string message, Exception inner)
        : base(message, inner)
    {
    }
}