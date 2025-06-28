using Store.Core.Contracts.Models;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Contracts;

public interface IItemEntityFactory
{
    ItemEntity Create(IItem item);
}