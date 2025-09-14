namespace Store.Core.Abstractions.Factories;

public interface IItemEntityFactory
{
    ItemEntity Create(IItem item);
}