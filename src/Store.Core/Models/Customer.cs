using Store.Core.Enums.Subscriptions;

namespace Store.Core.Models;

public class Customer
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public Subscription SubscriptionType { get; init; }
    
    public Customer(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        SubscriptionType = Subscription.None;
    }
    
    public Customer(Guid id, string name, string email, Subscription subscriptionType)
    {
        Id = id;
        Name = name;
        Email = email;
        SubscriptionType = subscriptionType;
    }
}