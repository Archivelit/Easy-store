namespace Store.App.GraphQl.Users;

public class UsersQueries : IGraphQlExtender
{
    [Authorize(Roles = new[] { "Admin", "User" })]
    public async Task<UserDto> GetUserById(
        [GraphQLName("input")] GetUserByIdQuery query,
        [Service] IQueryHandler<GetUserByIdQuery, UserDto> handler,
        CancellationToken ct)
    {
        return await handler.Handle(query, ct);
    }
}