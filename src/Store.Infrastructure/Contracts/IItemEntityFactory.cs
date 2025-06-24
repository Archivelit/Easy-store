using Store.Core.Contracts.Items;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Contracts;

public interface IItemEntityFactory
{
    ItemEntity Create(IItem item);
}