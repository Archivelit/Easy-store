namespace Store.Core.Models;

public class Item : IItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public Guid UserId { get; private set; }
    public int QuantityInStock { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; } = null;

    public Item(string title, decimal price, int quantityInStock, Guid userId, string? description = null) 
    {
        Description = description;
        Title = title;
        Price = price;
        QuantityInStock = quantityInStock;
        UserId = userId;
    }

    [JsonConstructor]
    public Item(Guid id, string title, decimal price, int quantityInStock, Guid userId, string? description, DateTime createdAt, DateTime? updatedAt)
        : this(title, price, quantityInStock, userId, description)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}