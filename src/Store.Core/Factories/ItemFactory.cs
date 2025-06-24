using Store.Core.Builders;
using Store.Core.Contracts.Items;
using Store.Core.Models;

namespace Store.Core.Factories;

public class ItemFactory : IItemFactory
{
    public Item Create(IItem item) => 
        new ItemBuilder().From(item).Build();
}