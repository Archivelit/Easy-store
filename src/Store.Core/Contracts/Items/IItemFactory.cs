using Store.App.GraphQl.Models;
using Store.Core.Models;

namespace Store.App.GraphQl.Items;

public interface IItemFactory
{
    Item Create(IItem item);
}