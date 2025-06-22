using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Infrastructure.Entities;

public class ItemEntity
{
    [Key]
    public Guid Id { get; internal set; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    public Guid CustomerId { get; set; }
    public int QuantityInStock { get; set; }
    
    [NotMapped]
    public bool IsAvailable => QuantityInStock > 0;
    public DateTime CreatedAt { get; internal set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public ItemEntity(string title, decimal price, int quantityInStock, Guid customerId,string? description = null)
    {
        Title = title;
        Price = price;
        QuantityInStock = quantityInStock;
        CustomerId = customerId;
        Description = description;
        UpdatedAt = null;
    }
    
    public ItemEntity(Guid id, string title, decimal price, int quantityInStock, Guid customerId,string? description, DateTime createdAt, DateTime? updatedAt)
    : this(title, price, quantityInStock, customerId, description)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
    
    internal ItemEntity(){}
}