using Store.App.GraphQl.CQRS;
using Store.Core.Models.Dto.Items;

namespace Store.Core.Contracts.CQRS.Item.Queries;

public record GetItemByIdQuery(Guid Id) : IQuery<ItemDto>; 