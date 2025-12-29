namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UpdateUserName : UpdateUserBase
{
    private readonly IValidator<string> _userNameValidator;

    public UpdateUserName(ILogger<UpdateUserName> logger, 
        [FromKeyedServices(KeyedServicesKeys.NameValidator)]IValidator<string> userNameValidator) : base(logger)
    {
        _userNameValidator = userNameValidator;
    }

    public override UserBuilder Update(UserBuilder builder, UpdateUserDto model)
    {
        if (model.Name != null)
        {
            _userNameValidator.ValidateAndThrow(model.Name);
            
            builder.WithName(model.Name);
            
            _logger.LogDebug("User {UserId} updated name to {NewUserName}", model.Id, model.Name);
        }
        return base.Update(builder, model);
    }
}