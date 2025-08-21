using FluentValidation;
using Microsoft.Extensions.Logging;
using Store.Core.Builders;
using Store.Core.Models.Dto.User;
using Store.Core.Utils.Validators.User;

namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UpdateUserEmail : UpdateUserBase
{
    public UpdateUserEmail(ILogger<UpdateUserEmail> logger) : base(logger) { }

    public override UserBuilder Update(UserBuilder builder, UserDto model)
    {
        if (model.Email != null)
        {
            new EmailValidator().Validate(model.Email, options =>
            {
                options.ThrowOnFailures();
            });
            builder.WithEmail(model.Email);
            _logger.LogDebug("User {UserId} updated email to {NewUserEmail}", model.Id, model.Email);
        }

        return base.Update(builder, model);
    }
}
