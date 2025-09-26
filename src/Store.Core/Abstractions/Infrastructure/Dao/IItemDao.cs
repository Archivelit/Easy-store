namespace Store.Core.Abstractions.Infrastructure.Dao;

/// <summary>
/// Implementation must operate with ef core. Needed to incapsulate ef core logic in repositories.
/// </summary>
public interface IItemDao
{
    /// <returns>
    /// Item entity model from database with specified id.
    /// </returns>
    Task<ItemEntity?> GetByIdAsync(Guid id);
    /// <summary>
    /// Register item entity in database.
    /// </summary>
    Task RegisterAsync(ItemEntity item);
    /// <summary>
    /// Update item model in database.
    /// </summary>
    /// <param name="item">
    /// Complete item entity model. Must be based on model in database for correct update.
    /// </param>
    Task UpdateAsync(ItemEntity item);
    /// <summary>
    /// Delete item model from database.
    /// </summary>
    Task DeleteAsync(ItemEntity item);
}