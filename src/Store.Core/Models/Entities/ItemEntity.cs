namespace Store.Core.Models.Entities;

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
    public Guid UserId { get; private set; }
    public int QuantityInStock { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; } = null;


    public ItemEntity(string title, decimal price, int quantityInStock, Guid userId, string? description = null)
    {
        Title = title;
        Price = price;
        QuantityInStock = quantityInStock;
        UserId = userId;
        Description = description;
    }
    
    public ItemEntity(Guid id, string title, decimal price, int quantityInStock, Guid userId,string? description, DateTime createdAt, DateTime? updatedAt)
    : this(title, price, quantityInStock, userId, description)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}