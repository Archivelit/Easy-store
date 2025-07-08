using Store.App.GraphQl.Models;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Builders;

public class ItemEntityBuilder
{
    private Guid Id { get; set; }
    private string Title { get; set; }
    private string? Description { get; set; }
    private decimal Price { get; set; }
    private Guid CustomerId { get; set; }
    private int QuantityInStock { get; set; }
    private DateTime CreatedAt { get; set; }
    private DateTime? UpdatedAt { get; set; }

    public ItemEntityBuilder() => InitDefault();

    public ItemEntity Build() => new(Id, Title, Price, QuantityInStock, CustomerId, Description, CreatedAt, UpdatedAt);

    public ItemEntityBuilder From(IItem item) =>
        WithId(item.Id)
        .WithTitle(item.Title)
        .WithDescription(item.Description)
        .WithPrice(item.Price)
        .WithQuantityInStock(item.QuantityInStock)
        .WithCustomerId(item.CustomerId)
        .WithCreatedAt(item.CreatedAt)
        .WithUpdatedAt(item.UpdatedAt);
    
    public ItemEntityBuilder Reset()
    {
        InitDefault();
        return this;
    }
    
    public ItemEntityBuilder WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public ItemEntityBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }
    
    public ItemEntityBuilder WithDescription(string? description)
    {
        Description = description;
        return this;
    }
    
    public ItemEntityBuilder WithPrice(decimal price)
    {
        Price = price;
        return this;
    }

    public ItemEntityBuilder WithCustomerId(Guid customerId)
    {
        CustomerId = customerId;
        return this;
    }

    public ItemEntityBuilder WithQuantityInStock(int quantityInStock)
    {
        QuantityInStock = quantityInStock;
        return this;
    }

    public ItemEntityBuilder WithUpdatedAt(DateTime? updatedAt)
    {
        UpdatedAt = updatedAt;
        return this;
    }

    public ItemEntityBuilder WithCreatedAt(DateTime createdAt)
    {
        CreatedAt = createdAt;
        return this;
    }

    public ItemEntityBuilder WithDefault()
    {
        Title = "Untitled";
        Price = 1m;
        CustomerId = Guid.NewGuid();
        QuantityInStock = 1;
        
        return this;
    }

    private void InitDefault()
    {
        Id = Guid.NewGuid();
        Title = string.Empty;
        Description = null;
        Price = 0;
        CustomerId = Guid.Empty;
        QuantityInStock = 0;
        CreatedAt = DateTime.Now;
        UpdatedAt = null;
    }
}