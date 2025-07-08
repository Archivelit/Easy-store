using Store.App.GraphQl.Models;
using Store.Core.Models;
using Store.Core.Models.Dto.Items;

namespace Store.Core.Contracts.Repositories;

public interface IItemRepository
{
    Task<Item> GetByIdAsync(Guid id);
    Task RegisterAsync(IItem item);
    Task DeleteByIdAsync(Guid id);
    Task UpdateAsync(ItemDto itemDto);
}