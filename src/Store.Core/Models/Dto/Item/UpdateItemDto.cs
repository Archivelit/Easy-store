namespace Store.Core.Models.Dto.Item;

public record UpdateItemDto(
    [property: Required] Guid Id,
    string? Title,
    string? Description,
    decimal? Price,
    int? QuantityInStock,
    string? ProfileImageUrl)
{
    public UpdateItemDto(IItem item) : this(item.Id, item.Title, item.Description, item.Price, item.QuantityInStock, item.ProfileImageUrl) { }
}