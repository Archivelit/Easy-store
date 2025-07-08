using Store.App.GraphQl.Models;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Contracts;

public interface IItemEntityFactory
{
    ItemEntity Create(IItem item);
}