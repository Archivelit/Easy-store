using Store.App.CQRS.Users.Commands.Update.UpdateChain;
using Store.Core.Builders;
using Store.Core.Contracts.Repositories;
using Store.Core.Models;
using Store.Core.Models.Dto.User;
using Microsoft.Extensions.Logging;
using Store.Core.Contracts.Validation;
using Store.Core.Contracts.Security;

namespace Store.App.CQRS.Users.Commands.Update;

public class UserUpdateFacade
{
    private readonly IUserUpdateChain _chain;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordValidator _passwordValidator;
    private readonly IPasswordHasher _passwordHasher;

    private readonly ILogger<UserUpdateFacade> _logger;

    public UserUpdateFacade(IUserUpdateChainFactory factory, IPasswordHasher passwordHasher, IPasswordValidator passwordValidator, IUserRepository userRepository, ILogger<UserUpdateFacade> logger)
    {
        _passwordHasher = passwordHasher;
        _passwordValidator = passwordValidator;
        _userRepository = userRepository;
        _chain = factory.Create();
        _logger = logger;
    }

    public async Task UpdateUserAsync(UserDto model, string password)
    {
        _logger.LogDebug("Updating user {UserId} in {method}", model.Id, nameof(UpdateUserAsync));

        var userData = await _userRepository.GetByIdAsync(model.Id);

        var builder = new UserBuilder();
        builder.From(userData.user);

        var updateData = GetNewData(builder, model, password);

        if (string.IsNullOrEmpty(updateData.passwordHash))
        {
            updateData.passwordHash = userData.passwordHash;
        }
        
        await _userRepository.UpdateAsync(updateData.customer, updateData.passwordHash);

        _logger.LogDebug("End updating user");
    }

    private (User customer, string passwordHash) GetNewData(UserBuilder builder, UserDto model, string password)
    {
        _logger.LogDebug("Trying to extract data for update");
        builder = _chain.Update(builder, model);
        var user = builder.Build();
        
        string passwordHash;
        if (!string.IsNullOrWhiteSpace(password))
        {
            _passwordValidator.ValidatePassword(password);
            passwordHash = _passwordHasher.HashPassword(password);
        }
        else
        {
            passwordHash = string.Empty;
        }

        _logger.LogDebug("Data extracted succesfuly");

        return (user, passwordHash);
    }
}