namespace Store.App.CQRS.Users.Queries;

public sealed class GetUserByIdQueryHandler (
    ILogger<GetUserByIdQueryHandler> logger,
    IUserRepository userRepository
    ) : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly ILogger<GetUserByIdQueryHandler> _logger = logger;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken ct)
    {
        _logger.LogDebug("User {UserId} requested", query.Id);

        var user = await _userRepository.GetByIdAsync(query.Id);

        _logger.LogDebug("User {UserId} returned", query.Id);

        return new(user);
    }
}