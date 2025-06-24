using Store.Core.Models;

namespace Store.Core.Contracts.Items;

public interface IItemFactory
{
    Item Create(IItem item);
}