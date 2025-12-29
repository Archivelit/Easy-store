namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

/// <summary>
/// Base class for user update chain. Inherit from this class to create new chain link and add to <see cref="UserUpdateChainFactory.Create"/> method to add own logic.
/// </summary>
public class UpdateUserBase : IUserUpdateChain
{
    protected IUserUpdateChain? _next;
    protected ILogger<UpdateUserBase> _logger;

    public UpdateUserBase(ILogger<UpdateUserBase> logger)
    {
        _logger = logger;
    }

    public IUserUpdateChain SetNext(IUserUpdateChain next)
    {
        _next = next;
        return next;
    }

    public virtual UserBuilder Update(UserBuilder builder, UpdateUserDto user)
    {
        if (_next != null)
            return _next.Update(builder, user);
        return builder;
    }
}