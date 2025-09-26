namespace Store.Core.Abstractions.Factories;

public interface IItemEntityFactory
{
    /// <summary>
    /// Creates item entity from any <see cref="IItem"/> implementation
    /// </summary>
    /// <returns>
    /// Entity model of item
    /// </returns>
    ItemEntity Create(IItem item);
}