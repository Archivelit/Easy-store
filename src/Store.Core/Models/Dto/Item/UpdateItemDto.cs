namespace Store.Core.Models.Dto.Items;

public record UpdateItemDto(
    [property: Required] Guid Id,
    string? Title,
    string? Description,
    decimal? Price,
    int? QuantityInStock,
    DateTime? UpdatedAt)
{
    public UpdateItemDto(IItem item) : this(item.Id, item.Title, item.Description, item.Price, item.QuantityInStock, item.UpdatedAt) { }
}