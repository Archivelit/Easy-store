namespace Store.App.GraphQl.Users;

using HotChocolate.Authorization;

public class UsersQueries : IGraphQlExtender
{
    [AllowAnonymous]
    public async Task<UserDto> GetUserById(
        [GraphQLName("input")] GetUserByIdQuery query,
        [Service] IQueryHandler<GetUserByIdQuery, UserDto> handler,
        CancellationToken ct)
    {
        return await handler.Handle(query, ct);
    }
}