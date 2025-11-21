namespace Store.Core.Abstractions.Repositories;

/// <summary>
/// Recomended for work with db.
/// <para>
/// Operates with <see cref="IItemDao"> and IDistributed cache implementations.
/// </para>
/// </summary>
public interface IItemRepository
{
    /// <summary>
    /// Search item in cache and db and return it if exists.<br/>
    /// Store item into cache if it was not stored.
    /// </summary>
    /// <returns>
    /// Item with specified id
    /// </returns>
    Task<Item> GetByIdAsync(Guid id);
    /// <summary>
    /// Saves item model in cache and database.
    /// </summary>
    /// <param name="item">
    /// Model to save
    /// </param>
    Task RegisterAsync(IItem item);
    /// <summary>
    /// Delete item with specified id from database (and cache if stored).
    /// </summary>
    Task DeleteByIdAsync(Guid id);
    /// <summary>
    /// Update item model in cache and database.
    /// </summary>
    /// <param name="item">
    /// Complete item entity model. Must be based on model from database for correct update.
    /// </param>
    Task UpdateAsync(IItem item);
}