using System.ComponentModel.DataAnnotations;
using Store.App.GraphQl.Models;

namespace Store.Core.Models.Dto.Items;

public record ItemDto : IItem
{
    public Guid Id { get; init; }
    [Required] public string Title { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public Guid UserId { get; init; }
    public int QuantityInStock { get; init; } 
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public ItemDto(Guid id, string title, string? description, decimal price, Guid customerId, int quantityInStock, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        UserId = customerId;
        QuantityInStock = quantityInStock;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public ItemDto(IItem item)
    {
        Id = item.Id;
        Title = item.Title;
        Description = item.Description;
        Price = item.Price;
        UserId = item.UserId;
        QuantityInStock = item.QuantityInStock;
        CreatedAt = item.CreatedAt;
        UpdatedAt = item.UpdatedAt;
    }
}