using Store.Core.Builders;
using Store.Infrastructure.Entities;
using Store.Core.Models;

namespace Store.Infrastructure.Mappers;

public static class ItemMapper
{
    public static Item ToDomain(ItemEntity item)
    {
        var builder = new ItemBuilder();

        return builder.WithTitle(item.Title)
            .WithDescription(item.Description)
            .WithId(item.Id)
            .WithPrice(item.Price)
            .WithCustomerId(item.CustomerId)
            .WithQuantityInStock(item.QuantityInStock)
            .WithCreatedAt(item.CreatedAt)
            .WithUpdatedAt(item.UpdatedAt)
            .Build();
    }

    public static ItemEntity ToEntity(Item item) => new(item.Id, item.Title, item.Price, item.QuantityInStock,
        item.CustomerId, item.Description, item.CreatedAt, item.UpdatedAt);
}