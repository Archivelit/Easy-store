using Store.App.GraphQl.Models;
using Store.Core.Exceptions.InvalidData.Item;

namespace Store.Core.Models;

public class Item : IItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public Guid UserId { get; private set; }
    public int QuantityInStock { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    internal Item(string title, decimal price, int quantityInStock, Guid customerId, string? description = null)
    {
        Id = Guid.NewGuid();
        Title = title;
        Price = price;
        QuantityInStock = quantityInStock;
        UserId = customerId;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
    }
    
    internal Item(Guid id, string title, decimal price, int quantityInStock, Guid customerId, string? description, DateTime createdAt, DateTime? updatedAt)
    {
        Id = id;
        Title = title;
        Price = price;
        QuantityInStock = quantityInStock;
        UserId = customerId;
        Description = description;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}