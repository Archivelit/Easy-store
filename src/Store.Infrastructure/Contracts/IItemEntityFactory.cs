namespace Store.Infrastructure.Contracts;

public interface IItemEntityFactory
{
    ItemEntity Create(IItem item);
}