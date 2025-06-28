using Store.Core.Models;
using Store.Core.Contracts.Models;

namespace Store.Core.Contracts.Items;

public interface IItemFactory
{
    Item Create(IItem item);
}