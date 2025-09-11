namespace Store.App.GraphQl.Items;

using HotChocolate.Authorization;

[ExtendObjectType("Query")]
public class ItemsQueries : IGraphQlExtender
{
    [AllowAnonymous]
    public async Task<ItemDto> GetItemById(
        [GraphQLName("input")]GetItemByIdQuery query,
        [Service] IQueryHandler<GetItemByIdQuery, ItemDto> handler, 
        CancellationToken cancellationToken)
    {
        return await handler.Handle(query, cancellationToken);
    }
}