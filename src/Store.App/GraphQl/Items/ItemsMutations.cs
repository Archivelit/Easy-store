using Store.App.GraphQl.CQRS;
using Store.Core.Contracts.CQRS.Items.Commands;
using Store.Core.Models.Dto.Items;

namespace Store.App.GraphQl.Items;

[ExtendObjectType("Mutation")]
public class ItemsMutations : IGraphQlExtender
{
    public async Task DeleteItem(
        [GraphQLName("input")] DeleteItemCommand command,
        [Service] ICommandHandler<DeleteItemCommand> handler, 
        CancellationToken cancellationToken)
    {
        await handler.Handle(command, cancellationToken);
    }
    
    public async Task<ItemDto> CreateItem(
        [GraphQLName("input")] CreateItemCommand command,
        [Service] ICommandHandler<CreateItemCommand, ItemDto> handler, 
        CancellationToken cancellationToken)
    {
        return await handler.Handle(command, cancellationToken);
    }

    public async Task<ItemDto> UpdateItem(
        [GraphQLName("input")] UpdateItemCommand command,
        [Service] ICommandHandler<UpdateItemCommand, ItemDto> handler,
        CancellationToken cancellationToken)
    {
        return await handler.Handle(command, cancellationToken);
    }
}