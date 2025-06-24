using Store.Core.Contracts.Items;

namespace Store.Core.Models.DTO.Items;

public record ItemDto : IItem
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public Guid CustomerId { get; init; }
    public int QuantityInStock { get; init; } 
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public ItemDto(Guid id, string title, string? description, decimal price, Guid customerId, int quantityInStock, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        CustomerId = customerId;
        QuantityInStock = quantityInStock;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}