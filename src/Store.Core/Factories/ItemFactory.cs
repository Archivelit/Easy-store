using Store.Core.Builders;
using Store.Core.Contracts.Items;
using Store.Core.Models;

namespace Store.Core.Factories;

public class ItemFactory
{
    private ItemBuilder _itemBuilder;

    public Item Create(IItem item)
    {
        _itemBuilder = new();
        
        var itemDomain = _itemBuilder.WithTitle(item.Title)
            .WithDescription(item.Description)
            .WithPrice(item.Price)
            .WithQuantityInStock(item.QuantityInStock)
            .WithCustomerId(item.CustomerId)
            .WithCreatedAt(item.CreatedAt)
            .WithUpdatedAt(item.UpdatedAt)
            .WithId(item.Id)
            .Build();

        _itemBuilder.Reset();
        
        return itemDomain;
    }
}