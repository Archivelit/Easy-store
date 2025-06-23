using Store.Core.Exceptions.InvalidData.Item;

namespace Store.Core.Models;

public class Item
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public Guid CustomerId { get; private set; }
    public int QuantityInStock { get; private set; }
    public bool IsAvailable => QuantityInStock > 0;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    internal Item(string title, decimal price, int quantityInStock, Guid customerId, string? description = null)
    {
        Id = Guid.NewGuid();
        Title = title;
        Price = price;
        QuantityInStock = quantityInStock;
        CustomerId = customerId;
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
        CustomerId = customerId;
        Description = description;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    
    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) 
            throw new InvalidItemTitle("Title cannot be empty");
        if (title.Length > 100)
            throw new InvalidItemTitle("Title length cannot exceed 100 characters");

        Title = title;
        MarkUpdated();
    }

    public void UpdatePrice(decimal price)
    {
        if (price < 0) 
            throw new InvalidItemPrice("Price cannot be negative");

        Price = price;
        MarkUpdated();
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity < 0)
            throw new InvalidItemQuantity("Quantity cannot be negative");

        QuantityInStock = quantity;
        MarkUpdated();
    }

    public void UpdateDescription(string? description)
    {
        if (description != null && description.Length > 1000)
            throw new InvalidItemDescription("Description length cannot exceed 1000 characters");

        Description = description;
        MarkUpdated();
    }

    private void MarkUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}