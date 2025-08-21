using Store.Core.Builders;
using Store.Core.Contracts.Factories;
using Store.Core.Models;

namespace Store.Core.Factories;

public class ItemDomainFactory : IItemFactory
{
    public Item Create(IItem item) => 
        new ItemBuilder().From(item).Build();
}