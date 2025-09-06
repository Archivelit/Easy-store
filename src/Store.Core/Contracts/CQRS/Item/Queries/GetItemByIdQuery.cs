namespace Store.Core.Contracts.CQRS.Item.Queries;

public record GetItemByIdQuery(Guid Id) : IQuery<ItemDto>; 