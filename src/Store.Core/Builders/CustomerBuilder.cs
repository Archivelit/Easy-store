using Store.App.GraphQl.Models;
using Store.Core.Enums.Subscriptions;
using Store.Core.Models;

namespace Store.Core.Builders;

public class CustomerBuilder
{
    private Guid Id { get; set; }
    private string Name { get; set; }
    private string Email { get; set; }
    private Subscription SubscriptionType { get; set; }

    public CustomerBuilder() => InitDefault();

    public CustomerBuilder Reset()
    {
        InitDefault();
        return this;
    }

    private CustomerBuilder InitDefault()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Email = string.Empty;
        SubscriptionType = Subscription.None;

        return this;
    }

    public CustomerBuilder From(ICustomer customer)
    {
        Id = customer.Id;
        Name = customer.Name;
        Email = customer.Email;
        SubscriptionType = customer.SubscriptionType;
        return this;
    }

    public CustomerBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }
    
    public CustomerBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public CustomerBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public CustomerBuilder WithSubscriptionType(Subscription subscriptionType)
    {
        SubscriptionType = subscriptionType;
        return this;
    }

    public Customer Build()
    {
        return new(Id, Name, Email, SubscriptionType);
    }
}