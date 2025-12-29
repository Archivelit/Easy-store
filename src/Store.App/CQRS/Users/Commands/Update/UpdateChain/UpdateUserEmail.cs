namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UpdateUserEmail : UpdateUserBase
{
    private readonly IValidator<string> _emailValidator;

    public UpdateUserEmail(ILogger<UpdateUserEmail> logger, 
        [FromKeyedServices(KeyedServicesKeys.EmailValidator)] IValidator<string> emailValidator) 
        : base(logger)
    {
        _emailValidator = emailValidator;
    }

    public override UserBuilder Update(UserBuilder builder, UpdateUserDto model)
    {
        if (model.Email != null)
        {
            _emailValidator.ValidateAndThrow(model.Email);

            builder.WithEmail(model.Email);

            _logger.LogDebug("User {UserId} updated email to {NewUserEmail}", model.Id, model.Email);
        }

        return base.Update(builder, model);
    }
}
