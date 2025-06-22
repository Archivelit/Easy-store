using Store.Infrastructure.Entities;
using Store.Core.Models;

namespace Store.Infrastructure.Mappers;

public static class ItemMapper
{
    public static Item ToDomain(ItemEntity item) => new(item.Id, item.Title, item.Price, item.QuantityInStock, 
        item.CustomerId, item.Description, item.CreatedAt, item.UpdatedAt);

    public static ItemEntity ToEntity(Item item) => new(item.Id, item.Title, item.Price, item.QuantityInStock,
        item.CustomerId, item.Description, item.CreatedAt, item.UpdatedAt);
}