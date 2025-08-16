using Microsoft.Extensions.Logging;
using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.User;

namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UpdateCustomerBase : IUserUpdateChain
{
    protected IUserUpdateChain? _next;
    protected IUserValidator _validator;
    protected ILogger _logger;

    public UpdateCustomerBase(IUserValidator validator, ILogger logger)
    {
        _validator = validator;
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