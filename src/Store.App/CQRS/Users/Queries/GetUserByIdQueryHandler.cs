namespace Store.App.CQRS.Users.Queries;

public sealed class GetUserByIdQueryHandler (
    IUserRepository userRepository
    ) : IQueryHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        return new(await userRepository.GetByIdAsync(query.Id));
    }
}