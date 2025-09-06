namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UpdateUserSubscription : UpdateUserBase
{
    public UpdateUserSubscription(ILogger<UpdateUserSubscription> logger) : base(logger)  { }

    public override UserBuilder Update(UserBuilder builder, UserDto model)
    {
        if (model.SubscriptionType != Subscription.None)
        {
            builder.WithSubscriptionType(model.SubscriptionType);

            _logger.LogDebug("User {UserId} updated email to {NewUserSubscription}", model.Id, model.SubscriptionType);
        }
        return base.Update(builder, model);
    }
}