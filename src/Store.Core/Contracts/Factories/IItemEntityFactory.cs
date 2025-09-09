namespace Store.Core.Factories;

public interface IItemEntityFactory
{
    ItemEntity Create(IItem item);
}