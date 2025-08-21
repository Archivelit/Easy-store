using Microsoft.Extensions.Logging;
using Store.Core.Builders;
using Store.Core.Models.Dto.User;

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

    public virtual UserBuilder Update(UserBuilder builder,UserDto model)
    {
        return _next.Update(builder, model);
    }
}