namespace Store.App.CQRS.Users.Commands.Update;

public sealed class UserUpdateFacade
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

    /// <summary>
    /// Updates user in the system with provided data. <br/>
    /// The model is based on the existing item in the database. 
    /// Whole logic of changing model is incapsulated in the chain. 
    /// See more in <see cref="UserUpdateChainFactory"/>
    /// </summary>
    /// <returns>
    /// Updated user model
    /// </returns>
    public async Task<UserDto> UpdateUserAsync(UpdateUserDto user)
    {
        _logger.LogDebug("Updating user {UserId} in {method}", user.Id, nameof(UpdateUserAsync));

        var userFromDb = await _userRepository.GetByIdAsync(user.Id);

        var builder = new UserBuilder();
        builder.From(userFromDb);

        var updatedUser = await GetNewData(builder, user);
        
        await _userRepository.UpdateAsync(updatedUser);

        _logger.LogDebug("End updating user {UserId}", user.Id);

        return new(updatedUser);
    }

    private async Task<User> GetNewData(UserBuilder builder, UpdateUserDto model)
    {
        _logger.LogDebug("Trying to extract data for update");
        
        builder = await _chain.Update(builder, model);
        var user = builder.Build();

        _logger.LogDebug("Data extracted successfully");

        return user;
    }
}