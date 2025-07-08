using Store.App.GraphQl.CQRS;
using Store.Core.Contracts.CQRS.Items.Queries;
using Store.Core.Models.Dto.Items;

namespace Store.App.GraphQl.Items;

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