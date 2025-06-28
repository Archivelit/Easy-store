using Store.Core.Contracts.Models;

namespace Store.Core.Models.Dto.Items;

public record CreateItemDto(string Title, string? Description, decimal Price, Guid CustomerId, int QuantityInStock) : IItem
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; init; } = null;
}