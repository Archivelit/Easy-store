namespace Store.Core.Builders;

public class ItemEntityBuilder
{
    private Guid Id { get; set; }
    private string Title { get; set; }
    private string? Description { get; set; }
    private decimal Price { get; set; }
    private Guid UserId { get; set; }
    private int QuantityInStock { get; set; }
    private DateTime CreatedAt { get; set; }
    private DateTime? UpdatedAt { get; set; }

#nullable disable
    public ItemEntityBuilder() => InitDefault();
#nullable enable

    public ItemEntity Build() => new(Id, Title, Price, QuantityInStock, UserId, Description, CreatedAt, UpdatedAt);

    /// <summary>
    /// Set the builder based on item in param.
    /// </summary>
    public ItemEntityBuilder From(IItem item) =>
        WithId(item.Id)
        .WithTitle(item.Title)
        .WithDescription(item.Description)
        .WithPrice(item.Price)
        .WithQuantityInStock(item.QuantityInStock)
        .WithUserId(item.UserId)
        .WithCreatedAt(item.CreatedAt)
        .WithUpdatedAt(item.UpdatedAt);

    /// <summary>
    /// Reset the builder properties.
    /// </summary>
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

    public ItemEntityBuilder WithUserId(Guid userId)
    {
        UserId = userId;
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

    /// <summary>
    /// "Seed" the builder fields with some parameters. Not recomended to use in core logic, made for tests only.
    /// </summary>
    public ItemEntityBuilder WithDefault()
    {
        Title = "Untitled";
        Price = 1m;
        UserId = Guid.NewGuid();
        QuantityInStock = 1;

        return this;
    }

    /// <summary>
    /// Initialize builders defaults. Used to reset builder properties.
    /// </summary>
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