namespace Store.Core.Builders;

#nullable disable
public class ItemBuilder
{
    private Guid Id { get; set; }
    private string Title { get; set; }
    private string Description { get; set; }
    private decimal Price { get; set; }
    private Guid UserId { get; set; }
    private int QuantityInStock { get; set; }
    private DateTime CreatedAt { get; set; }
    private DateTime? UpdatedAt { get; set; }

    public ItemBuilder() => InitDefault();

    public Item Build() => new(Id, Title, Price, QuantityInStock, UserId, Description, CreatedAt, UpdatedAt);

    public ItemBuilder From(IItem item) =>
        WithId(item.Id)
        .WithTitle(item.Title)
        .WithDescription(item.Description)
        .WithPrice(item.Price)
        .WithQuantityInStock(item.QuantityInStock)
        .WithUserId(item.UserId)
        .WithCreatedAt(item.CreatedAt)
        .WithUpdatedAt(item.UpdatedAt);

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

    public ItemBuilder WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public ItemBuilder WithPrice(decimal price)
    {
        Price = price;
        return this;
    }

    public ItemBuilder WithUserId(Guid userId)
    {
        UserId = userId;
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

    public ItemBuilder UpdatedNow()
    {
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public ItemBuilder WithDefault()
    {
        Title = "Untitled";
        Price = 1m;
        UserId = Guid.NewGuid();
        QuantityInStock = 1;

        return this;
    }

    private void InitDefault()
    {
        Id = Guid.NewGuid();
        Title = string.Empty;
        Description = null;
        Price = 0;
        UserId = Guid.Empty;
        QuantityInStock = 0;
        CreatedAt = DateTime.Now;
        UpdatedAt = null;
    }
}