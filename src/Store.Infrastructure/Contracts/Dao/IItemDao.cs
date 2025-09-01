using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Contracts.Dao;

public interface IItemDao
{
    Task<ItemEntity?> GetByIdAsync(Guid id);
    Task RegisterAsync(ItemEntity item);
    Task UpdateAsync(ItemEntity item);
    Task DeleteAsync(ItemEntity item);
}