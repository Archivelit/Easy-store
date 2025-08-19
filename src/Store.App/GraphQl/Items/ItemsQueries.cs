using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.Item.Queries;
using Store.Core.Models.Dto.Items;

namespace Store.App.GraphQl.Factories;

[ExtendObjectType("Query")]
public class ItemsQueries : IGraphQlExtender
{
    public async Task<ItemDto> GetItemById(
        [GraphQLName("input")]GetItemByIdQuery query,
        [Service] IQueryHandler<GetItemByIdQuery, ItemDto> handler, 
        CancellationToken cancellationToken)
    {
        return await handler.Handle(query, cancellationToken);
    }
}