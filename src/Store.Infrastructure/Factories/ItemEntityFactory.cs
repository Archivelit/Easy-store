namespace Store.Infrastructure.Factories;

public class ItemEntityFactory : IItemEntityFactory
{
    public ItemEntity Create(IItem item) => 
        new ItemEntityBuilder().From(item).Build();
}