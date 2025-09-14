namespace Store.Core.Abstractions.Repositories;

public interface IItemRepository
{
    Task<Item> GetByIdAsync(Guid id);
    Task RegisterAsync(IItem item);
    Task DeleteByIdAsync(Guid id);
    Task UpdateAsync(IItem item);
}