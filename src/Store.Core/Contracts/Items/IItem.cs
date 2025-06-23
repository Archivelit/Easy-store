namespace Store.Core.Contracts.Items;

public interface IItem
{
    Guid Id { get; }
    string Title { get; }
    string? Description { get; }
    decimal Price { get; }
    Guid CustomerId { get; }
    int QuantityInStock { get; }
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
}