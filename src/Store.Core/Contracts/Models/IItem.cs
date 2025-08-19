namespace Store.Core.Models;

public interface IItem
{
    Guid Id { get; }
    string Title { get; }
    string? Description { get; }
    decimal Price { get; }
    Guid UserId { get; }
    int QuantityInStock { get; }
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
}