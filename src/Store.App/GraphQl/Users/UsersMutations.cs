namespace Store.App.GraphQl.Users;

[ExtendObjectType("Mutation")]
public class UsersMutations : IGraphQlExtender  
{
    [Authorize("Admin")]
    public async Task<UserDto> RegisterUser(
        [GraphQLName("input")] RegisterUserCommand command, 
        [Service] ICommandHandler<RegisterUserCommand, UserDto> handler, 
        CancellationToken ct)
    {
        return await handler.Handle(command, ct);
    }

    [Authorize("Admin")]
    public async Task DeleteUser(
        [GraphQLName("input")] DeleteUserCommand command,
        [Service] ICommandHandler<DeleteUserCommand> handler,
        CancellationToken ct)
    {
        await handler.Handle(command, ct);
    }

    [Authorize(Roles = new[] { "Admin", "User"})]
    public async Task UpdateUser(
        [GraphQLName("input")] UpdateUserCommand command,
        [Service] ICommandHandler<UpdateUserCommand, UserDto> handler,
        CancellationToken ct)
    {
        await handler.Handle(command, ct);
    }
}