namespace Store.App.CQRS.Users.Commands.Update;

public class UserUpdateFacade
{
    private readonly IUserUpdateChain _chain;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserUpdateFacade> _logger;

    public UserUpdateFacade(IUserUpdateChainFactory factory, IUserRepository userRepository, ILogger<UserUpdateFacade> logger)
    {
        _userRepository = userRepository;
        _chain = factory.Create();
        _logger = logger;
    }

    public async Task<UserDto> UpdateUserAsync(UserDto user)
    {
        _logger.LogDebug("Updating user {UserId} in {method}", user.Id, nameof(UpdateUserAsync));

        var userFromDb = await _userRepository.GetByIdAsync(user.Id);

        var builder = new UserBuilder();
        builder.From(userFromDb);

        var updatedUser = GetNewData(builder, user);
        
        await _userRepository.UpdateAsync(updatedUser);

        _logger.LogDebug("End updating user {UserId}", user.Id);

        return new(updatedUser);
    }

    private User GetNewData(UserBuilder builder, UserDto model)
    {
        _logger.LogDebug("Trying to extract data for update");
        
        builder = _chain.Update(builder, model);
        var user = builder.Build();

        _logger.LogDebug("Data extracted succesfuly");

        return user;
    }
}