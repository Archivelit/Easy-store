using Store.App.GraphQl.Validation;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Utils.Validators.User;

namespace Store.Tests.Core.Utils.Validators;

#nullable enable

public class SubscriptionValidatorTests
{
    private readonly ISubscriptionValidator _subscriptionValidator = new SubscriptionValidator();

    [Theory]
    
    [InlineData("None")]
    [InlineData("none")]
    [InlineData("NONE ")]
    [InlineData("NONE")]
    [InlineData("StorePlus")]
    [InlineData("StorePro")]
    
    public void SubscriptionValidatorTests_ValidTests(string subscription)
    {
        ValidateSubscription(subscription);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void SubscriptionValidatorTests_InvalidTests(string subscription)
    {
        Assert.Throws<InvalidSubscription>(() => _subscriptionValidator.ValidateSubscription(subscription));
    }
    private void ValidateSubscription(string subscription)
    {
        _subscriptionValidator.ValidateSubscription(subscription);
    }
}