namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UpdateUserName : UpdateUserBase
{
    public UpdateUserName(ILogger<UpdateUserName> logger) : base(logger) { }

    public override async Task<UserBuilder> Update(UserBuilder builder, UpdateUserDto model)
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
        return await base.Update(builder, model);
    }
}