namespace Store.Core.Contracts.Validation;

public interface ISubscriptionValidator
{
    void ValidateSubscription(string subscription);
}