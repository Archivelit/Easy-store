using Microsoft.Extensions.Logging;
using Store.Core.Builders;
using Store.Core.Contracts.Validation;
using Store.Core.Models.Dto.User;

namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UpdateUserEmail : UpdateCustomerBase
{
    public UpdateUserEmail(IUserValidator validator, ILogger logger) : base(validator, logger) { }

    public override UserBuilder Update(UserBuilder builder, UserDto model)
    {
        if (model.Email != null)
        {
            _validator.ValidateEmail(model.Email);
            builder.WithEmail(model.Email);
            _logger.LogDebug("User {UserId} updated email to {NewUserEmail}", model.Id, model.Email);
        }

        return base.Update(builder, model);
    }
}
