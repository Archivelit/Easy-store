using Microsoft.Extensions.Logging;
using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.User;
using Store.Core.Enums.Subscriptions;

namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public class UpdateUserSubscription : UpdateCustomerBase
{
    public UpdateUserSubscription(IUserValidator validator, ILogger logger) : base(validator, logger)  { }

    public override UserBuilder Update(UserBuilder builder, UserDto model)
    {
        if (model.SubscriptionType != Subscription.None)
        {
            _validator.ValidateSubscription(model.SubscriptionType.ToString());
            builder.WithSubscriptionType(model.SubscriptionType);

            _logger.LogDebug("User {UserId} updated email to {NewUserSubscription}", model.Id, model.SubscriptionType);
        }
        return base.Update(builder, model);
    }
}