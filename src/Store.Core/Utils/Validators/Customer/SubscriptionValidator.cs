using Serilog;
using Store.App.GraphQl.Validation;
using Store.Core.Enums.Subscriptions;
using Store.Core.Exceptions.InvalidData;

namespace Store.Core.Utils.Validators.Customer;

public class SubscriptionValidator : ISubscriptionValidator
{
    public void ValidateSubscription(string subscription)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(subscription);
        
        if (!Enum.TryParse(subscription, true, out Subscription subscriptionValidationResult)) 
            throw new InvalidSubscription("Subscription is invalid.");

        Log.Debug("User subscription validated succesfuly");
    }
}