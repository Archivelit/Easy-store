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
    public string ProfileImageUrl { get; private set; }

    public Item(string title, decimal price, int quantityInStock, Guid userId, string? description = null, string? profileImageUrl = null) 
    {
        Description = description;
        Title = title;
        Price = price;
        QuantityInStock = quantityInStock;
        UserId = userId;
        ProfileImageUrl = profileImageUrl ?? MinIO.DEFAULT_IMAGE_URL;
    }

    public Item(IItem item) : this(item.Title, item.Price, item.QuantityInStock, item.UserId, item.Description, item.ProfileImageUrl) 
    { 
        Id = item.Id;
        CreatedAt = item.CreatedAt;
        UpdatedAt = item.UpdatedAt;
    }

    [JsonConstructor]
    public Item(Guid id, string title, decimal price, int quantityInStock, Guid userId, string? description, string? profileImageUrl, DateTime createdAt, DateTime? updatedAt)
        : this(title, price, quantityInStock, userId, description, profileImageUrl)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}