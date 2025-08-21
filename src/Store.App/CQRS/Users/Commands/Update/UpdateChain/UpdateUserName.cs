using FluentValidation;
using Microsoft.Extensions.Logging;
using Store.Core.Builders;
using Store.Core.Models.Dto.User;
using Store.Core.Utils.Validators.User;

namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UpdateUserName : UpdateUserBase
{
    public UpdateUserName(ILogger<UpdateUserName> logger) : base(logger) { }

    public override UserBuilder Update(UserBuilder builder, UserDto model)
    {
        if (model.Name != null)
        {
            new NameValidator().Validate(model.Name, options =>
            {
                options.ThrowOnFailures();
            });
            builder.WithName(model.Name);
            _logger.LogDebug("User {UserId} updated name to {NewUserName}", model.Id, model.Name);
        }
        return base.Update(builder, model);
    }
}