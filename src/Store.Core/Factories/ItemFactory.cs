using Store.Core.Builders;
using Store.App.GraphQl.Factories;
using Store.App.GraphQl.Models;
using Store.Core.Models;

namespace Store.Core.Factories;

public class ItemFactory : IItemFactory
{
    public Item Create(IItem item) => 
        new ItemBuilder().From(item).Build();
}