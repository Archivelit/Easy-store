namespace Store.Core.Factories;

public class ItemEntityFactory : IItemEntityFactory
{
    public ItemEntity Create(IItem item) => 
        new ItemEntityBuilder().From(item).Build();
}