using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Store.Core.Contracts.Models;
using Store.Core.Exceptions.InvalidData.Item;

namespace Store.Infrastructure.Entities;

public class ItemEntity : IItem
{
    [Key]
    public Guid Id { get; private set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(100)]
    public string Title { get; private set; }
    
    [MaxLength(1000)]
    public string? Description { get; private set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; private set; }
    public Guid CustomerId { get; private set; }
    public int QuantityInStock { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    
    public ItemEntity(string title, decimal price, int quantityInStock, Guid customerId,string? description = null)
    {
        UpdateTitle(title);
        UpdatePrice(price);
        SetQuantity(quantityInStock);
        CustomerId = customerId;
        UpdateDescription(description);
        UpdatedAt = null;
    }
    
    public ItemEntity(Guid id, string title, decimal price, int quantityInStock, Guid customerId,string? description, DateTime createdAt, DateTime? updatedAt)
    : this(title, price, quantityInStock, customerId, description)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    
    private ItemEntity(){}
    
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

    public void SetQuantity(int quantity)
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

    private void MarkUpdated() => UpdatedAt = DateTime.UtcNow;
}