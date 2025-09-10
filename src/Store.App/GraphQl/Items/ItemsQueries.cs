namespace Store.App.GraphQl.Factories;

[ExtendObjectType("Query")]
public class ItemsQueries : IGraphQlExtender
{
    [Authorize(Roles = new[] { "User", "Admin" })]
    public async Task<ItemDto> GetItemById(
        [GraphQLName("input")]GetItemByIdQuery query,
        [Service] IQueryHandler<GetItemByIdQuery, ItemDto> handler, 
        CancellationToken cancellationToken)
    {
        return await handler.Handle(query, cancellationToken);
    }
}