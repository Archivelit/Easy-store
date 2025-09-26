namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UpdateUserEmail : UpdateUserBase
{
    private readonly IUserRepository _repository;

    public UpdateUserEmail(ILogger<UpdateUserEmail> logger, IUserRepository repository) : base(logger) 
    {
        _repository = repository;
    }

    public override async Task<UserBuilder> Update(UserBuilder builder, UpdateUserDto model)
    {
        if (model.Email != null)
        {
            new EmailValidator().Validate(model.Email, options =>
            {
                options.ThrowOnFailures();
            });

            if (await _repository.IsEmailClaimedAsync(model.Email))
            {
                throw new InvalidUserDataException($"Email {model.Email} already in use");
            }

            builder.WithEmail(model.Email);
            _logger.LogDebug("User {UserId} updated email to {NewUserEmail}", model.Id, model.Email);
        }

        return await base.Update(builder, model);
    }
}
