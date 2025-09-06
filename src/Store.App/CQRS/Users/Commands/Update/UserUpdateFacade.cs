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

    public async Task UpdateUserAsync(UserDto model)
    {
        _logger.LogDebug("Updating user {UserId} in {method}", model.Id, nameof(UpdateUserAsync));

        var userData = await _userRepository.GetByIdAsync(model.Id);

        var builder = new UserBuilder();
        builder.From(userData);

        var updateData = GetNewData(builder, model);
        
        await _userRepository.UpdateAsync(updateData);

        _logger.LogDebug("End updating user");
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