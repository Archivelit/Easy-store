namespace Store.Core.Models.Dto.Item;

public record CreateItemDto(string Title, string? Description, decimal Price, Guid UserId, int QuantityInStock) : IItem
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; init; } = null;
    public string? ProfileImageUrl { get; init; } = MinIO.DEFAULT_IMAGE_URL;
}