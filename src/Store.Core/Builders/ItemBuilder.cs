using Store.Core.Exceptions.InvalidData.Item;
using Store.Core.Models;

namespace Store.Core.Builders;

public class ItemBuilder
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public Guid CustomerId { get; private set; }
    public int QuantityInStock { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public ItemBuilder() => InitDefault();

    public Item Build()
    {
        ValidateAndThrow();
        
        return new(Id, Title, Price, QuantityInStock, CustomerId, Description, CreatedAt, UpdatedAt);
    }

    public ItemBuilder Reset()
    {
        InitDefault();
        return this;
    }

    public ItemBuilder WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public ItemBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }
    
    public ItemBuilder WithDescription(string? description)
    {
        Description = description;
        return this;
    }
    
    public ItemBuilder WithPrice(decimal price)
    {
        Price = price;
        return this;
    }

    public ItemBuilder WithCustomerId(Guid customerId)
    {
        CustomerId = customerId;
        return this;
    }

    public ItemBuilder WithQuantityInStock(int quantityInStock)
    {
        QuantityInStock = quantityInStock;
        return this;
    }

    public ItemBuilder WithUpdatedAt(DateTime? updatedAt)
    {
        UpdatedAt = updatedAt;
        return this;
    }

    public ItemBuilder WithCreatedAt(DateTime createdAt)
    {
        CreatedAt = createdAt;
        return this;
    }

    public ItemBuilder WithDefault()
    {
        Title = "Untitled";
        Price = 1m;
        CustomerId = Guid.NewGuid();
        QuantityInStock = 1;
        
        return this;
    }

    private void ValidateAndThrow()
    {
        if (String.IsNullOrWhiteSpace(Title)) 
            throw new InvalidItemTitle("Item title cannot be empty");
        if (Price <= 0) 
            throw new InvalidItemPrice("Price must be greater than zero");
        if (CustomerId == Guid.Empty)
            throw new InvalidCustomerId("Customer ID cannot be empty");
        
        if(QuantityInStock <= 0)
            throw new InvalidItemQuantity("Quantity must be greater than zero");
        
        if (Title.Length > 100)
            throw new InvalidItemTitle("Title must be less than 100 characters");
        if (Title.Length < 3)
            throw new InvalidItemTitle("Title must be at least 3 characters");
        
        if (Description != null && Description.Length > 1000)
            throw new InvalidItemDescription("Description must be less than 1000 characters");
        
        if (UpdatedAt < CreatedAt)
            throw new InvalidItemUpdateTime("Item update time must be greater than CreatedAt");
        
        if (CreatedAt > DateTime.Now)
            throw new InvalidItemCreateTime("Creation time cannot be in the future");
    }

    private void InitDefault()
    {
        Id = Guid.NewGuid();
        Title = null;
        Description = null;
        Price = 0;
        CustomerId = Guid.Empty;
        QuantityInStock = 0;
        CreatedAt = DateTime.Now;
        UpdatedAt = null;
    }
}