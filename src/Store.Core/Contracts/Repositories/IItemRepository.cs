using Store.Core.Contracts.Models;
using Store.Core.Models;

namespace Store.Core.Contracts.Repositories;

public interface IItemRepository
{
    Task<Item> GetItemByIdAsync(Guid id);
    Task RegisterItemAsync(IItem item);
    Task DeleteItemByIdAsync(Guid id);
}