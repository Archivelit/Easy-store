namespace Store.Core.Abstractions.Factories;

public interface IItemFactory
{
    /// <summary>
    /// Creates domain item from any <see cref="IItem"/> implementation
    /// </summary>
    /// <returns>
    /// Domain model of item
    /// </returns>
    Item Create(IItem item);
}