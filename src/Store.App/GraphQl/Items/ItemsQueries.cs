using Serilog;

namespace Store.App.GraphQl.Items;

[ExtendObjectType(typeof(Query))]
public class ItemsQueries : IGraphQlExtender
{
    public async Task<ItemDto> GetItemById(
        [GraphQLName("input")] GetItemByIdQuery query,
        IQueryHandler<GetItemByIdQuery, ItemDto> handler, 
        CancellationToken cancellationToken)
    {
        Log.Logger.Debug(handler.GetType().Name);
        return await handler.Handle(query, cancellationToken);
    }
}