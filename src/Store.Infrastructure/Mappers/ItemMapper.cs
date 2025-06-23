using Store.Core.Builders;
using Store.Core.Models;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Mappers;

public static class ItemMapper
{
    public static Item ToDomain(ItemEntity item)
    {
        var builder = new ItemBuilder();

        return builder.WithId(item.Id)
            .WithTitle(item.Title)
            .WithDescription(item.Description)
            .WithCreatedAt(item.CreatedAt)
            .WithUpdatedAt(item.UpdatedAt)
            .WithPrice(item.Price)
            .WithQuantityInStock(item.QuantityInStock)
            .WithCustomerId(item.CustomerId)
            .Build();
    }

    public static ItemEntity ToEntity(Item item) =>
        new(item.Id, item.Title, item.Price, item.QuantityInStock, item.CustomerId, item.Description, item.CreatedAt, item.UpdatedAt);
}