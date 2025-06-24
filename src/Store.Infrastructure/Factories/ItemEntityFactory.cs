using Store.Core.Contracts.Items;
using Store.Infrastructure.Builders;
using Store.Infrastructure.Contracts;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Factories;

public class ItemEntityFactory : IItemEntityFactory
{
    public ItemEntity Create(IItem item) => 
        new ItemEntityBuilder().From(item).Build();
}