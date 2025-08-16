using Store.App.GraphQl.Models;
using Store.Core.Enums.Subscriptions;

namespace Store.Core.Models;

public class User : IUser
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public Subscription SubscriptionType { get; init; }
    
    public User(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        SubscriptionType = Subscription.None;
    }
    
    public User(Guid id, string name, string email, Subscription subscriptionType)
    {
        Id = id;
        Name = name;
        Email = email;
        SubscriptionType = subscriptionType;
    }
}