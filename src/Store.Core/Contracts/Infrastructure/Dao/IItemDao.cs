namespace Store.Core.Contracts.Infrastructure.Dao;

public interface IItemDao
{
    Task<ItemEntity?> GetByIdAsync(Guid id);
    Task RegisterAsync(ItemEntity item);
    Task UpdateAsync(ItemEntity item);
    Task DeleteAsync(ItemEntity item);
}