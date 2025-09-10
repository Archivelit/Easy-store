namespace Store.App.GraphQl.Factories;

[ExtendObjectType("Mutation")]
public class ItemsMutations : IGraphQlExtender
{
    [Authorize(Roles = new[] { "User", "Admin" })]
    public async Task DeleteItem(
        [GraphQLName("input")] DeleteItemCommand command,
        [Service] ICommandHandler<DeleteItemCommand> handler, 
        CancellationToken cancellationToken)
    {
        await handler.Handle(command, cancellationToken);
    }

    [Authorize(Roles = new[] { "User", "Admin" })]
    public async Task<ItemDto> CreateItem(
        [GraphQLName("input")] CreateItemCommand command,
        [Service] ICommandHandler<CreateItemCommand, ItemDto> handler, 
        CancellationToken cancellationToken)
    {
        return await handler.Handle(command, cancellationToken);
    }

    [Authorize(Roles = new[] { "User", "Admin" })]
    public async Task<ItemDto> UpdateItem(
        [GraphQLName("input")] UpdateItemCommand command,
        [Service] ICommandHandler<UpdateItemCommand, ItemDto> handler,
        CancellationToken cancellationToken)
    {
        return await handler.Handle(command, cancellationToken);
    }
}