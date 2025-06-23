using Store.Core.Exceptions.InvalidData.Item;

namespace Store.Core.Exceptions.InvalidData.Item;

public class InvalidItemUpdateTime : InvalidItemData
{
    public InvalidItemUpdateTime(string message)
        : base(message)
    {
    }
    
    public InvalidItemUpdateTime(string message, Exception inner)
        : base(message, inner)
    {
    }
}