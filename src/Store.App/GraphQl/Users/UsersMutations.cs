using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.User.Commands;
using Store.Core.Models.Dto.User;

namespace Store.App.GraphQl.Users;

[ExtendObjectType("Mutation")]
public class UsersMutations : IGraphQlExtender  
{
    public async Task<UserDto> RegisterCustomer(
        [GraphQLName("input")] RegisterUserCommand command, 
        [Service] ICommandHandler<RegisterUserCommand, UserDto> handler, 
        CancellationToken ct)
    {
        return await handler.Handle(command, ct);
    }

    public async Task<string> AuthenticateCustomer(
        [GraphQLName("input")] AuthenticateUserCommand command, 
        [Service] ICommandHandler<AuthenticateUserCommand, string> handler, 
        CancellationToken ct)
    {
        return await handler.Handle(command, ct);
    }

    public async Task DeleteCustomer(
        [GraphQLName("input")] DeleteUserCommand command,
        [Service] ICommandHandler<DeleteUserCommand> handler,
        CancellationToken ct)
    {
        await handler.Handle(command, ct);
    }

    public async Task UpdateCustomer(
        [GraphQLName("input")] UpdateUserCommand command,
        [Service] ICommandHandler<UpdateUserCommand> handler,
        CancellationToken ct)
    {
        await handler.Handle(command, ct);
    }
}