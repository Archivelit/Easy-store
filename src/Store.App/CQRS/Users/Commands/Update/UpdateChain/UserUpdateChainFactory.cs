namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UserUpdateChainFactory : IUserUpdateChainFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UserUpdateChainFactory> _logger;

    public UserUpdateChainFactory(IServiceProvider serviceProvider, ILogger<UserUpdateChainFactory> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public IUserUpdateChain Create()
    {
        _logger.LogDebug("Creating user update chain");

        var emailHandler = _serviceProvider.GetRequiredService<UpdateUserEmail>();
        var nameHandler = _serviceProvider.GetRequiredService<UpdateUserName>();
        var subscriptionHandler = _serviceProvider.GetRequiredService<UpdateUserSubscription>();

        emailHandler.SetNext(nameHandler).SetNext(subscriptionHandler);

        _logger.LogDebug("User update chain created");

        return emailHandler;
    }
}
