namespace Store.Core.Models;

public class User : IUser
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; }
    public string Email { get; init; }
    public Subscription SubscriptionType { get; init; } = Subscription.None;
    
    public User(string name, string email)
    {
        Name = name;
        Email = email;
    }

    [JsonConstructor]
    public User(Guid id, string name, string email, Subscription subscriptionType)
    : this (name, email)
    {
        Id = id;
        SubscriptionType = subscriptionType;
    }

    public User(IUser user) : this(user.Id, user.Name, user.Email, user.SubscriptionType) { }
}