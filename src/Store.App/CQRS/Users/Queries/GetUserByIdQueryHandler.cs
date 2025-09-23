namespace Store.App.CQRS.Users.Queries;

public sealed class GetUserByIdQueryHandler (
    IUserRepository userRepository
    ) : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var user = await _userRepository.GetByIdAsync(query.Id);
        return new(user);
    }
}