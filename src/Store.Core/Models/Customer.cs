using Store.Core.Enums.Subscriptions;

namespace Store.Core.Models;

public class Customer
{
    public Guid Id { get; internal set; }
    public string Name { get; internal set; }
    public string Email { get; internal set; }
    public Subscription SubscriptionType { get; internal set; } 
    
    internal Customer(){}
    
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