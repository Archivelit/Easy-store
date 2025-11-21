namespace Store.App.CQRS.Models.Item.Queries;

public record GetItemByIdQuery(Guid Id) : IQuery<ItemDto>; 