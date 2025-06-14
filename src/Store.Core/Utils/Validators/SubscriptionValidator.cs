using Store.Core.Contracts.Validation;
using Store.Core.Enums.Subscriptions;
using Store.Core.Exceptions.InvalidData;

namespace Store.Core.Utils.Validators;

public class SubscriptionValidator : ISubscriptionValidator
{
    public void ValidateSubscription(string subscription)
    {
        if (string.IsNullOrEmpty(subscription))
            throw new InvalidSubscriptionException("Subscription is null or empty.");
        
        if (!Enum.TryParse(subscription, true, out Subscription subscriptionValidationResult)) 
            throw new InvalidSubscriptionException("Subscription is invalid.");
    }
}