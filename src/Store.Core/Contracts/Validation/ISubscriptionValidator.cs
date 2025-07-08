namespace Store.App.GraphQl.Validation;

public interface ISubscriptionValidator
{
    void ValidateSubscription(string subscription);
}