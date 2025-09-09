using Store.Core.Models.Dto.Item;

namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

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

    public virtual UserBuilder Update(UserBuilder builder, UserDto user)
    {
        if (_next != null)
            return _next.Update(builder, user);
        return builder;
    }
}