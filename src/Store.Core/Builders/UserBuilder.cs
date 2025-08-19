using Store.Core.Contracts.Models;
using Store.Core.Enums.Subscriptions;
using Store.Core.Models;

namespace Store.Core.Builders;

public class UserBuilder
{
    private Guid Id { get; set; }
    private string Name { get; set; }
    private string Email { get; set; }
    private Subscription SubscriptionType { get; set; }

    public UserBuilder() => InitDefault();

    public UserBuilder Reset()
    {
        InitDefault();
        return this;
    }

    private UserBuilder InitDefault()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Email = string.Empty;
        SubscriptionType = Subscription.None;

        return this;
    }

    public UserBuilder From(IUser user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
        SubscriptionType = user.SubscriptionType;
        return this;
    }

    public UserBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }
    
    public UserBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public UserBuilder WithSubscriptionType(Subscription subscriptionType)
    {
        SubscriptionType = subscriptionType;
        return this;
    }

    public User Build()
    {
        return new(Id, Name, Email, SubscriptionType);
    }
}