using Microsoft.Extensions.Logging;
using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.User;

namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UpdateUserName : UpdateCustomerBase
{
    public UpdateUserName(IUserValidator validator, ILogger logger) : base(validator, logger) { }

    public override UserBuilder Update(UserBuilder builder, UserDto model)
    {
        if (model.Name != null)
        {
            _validator.ValidateCustomerName(model.Name);
            builder.WithName(model.Name);
            _logger.LogDebug("User {UserId} updated name to {NewUserName}", model.Id, model.Name);
        }
        return base.Update(builder, model);
    }
}