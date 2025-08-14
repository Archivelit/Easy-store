using Microsoft.Extensions.Logging;
using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.Customers;
using Store.Core.Enums.Subscriptions;

namespace Store.App.CQRS.Customers.Commands.Update.UpdateChain;

public class UpdateCustomerSubscription : UpdateCustomerBase
{
    public UpdateCustomerSubscription(ICustomerValidator validator, ILogger logger) : base(validator, logger)  { }

    public override CustomerBuilder Update(CustomerBuilder builder, CustomerDto model)
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