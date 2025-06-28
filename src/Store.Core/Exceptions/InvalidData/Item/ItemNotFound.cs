namespace Store.Core.Exceptions.InvalidData.Item;

public class ItemNotFound : Exception
{
    public ItemNotFound(string message) : base(message) { }
    
    public ItemNotFound(string message, Exception inner) : base(message, inner) { }
}