using System.ComponentModel.DataAnnotations;
using Store.Core.Enums.Subscriptions;

namespace Store.Core.Models;

public class CustomerEntity
{
    [Key]
    public Guid Id { get; internal set; }
    public string Name { get; internal set; }
    public string Email { get; internal set; }
    public Subscription SubscriptionType { get; internal set; }
    public string PasswordHash { get; internal set; }
    
    public CustomerEntity(Guid id, string name, string email, Subscription subscriptionType, string passwordHash)
    {
        Id = id;
        Name = name;
        Email = email;
        SubscriptionType = subscriptionType;
        PasswordHash = passwordHash;
    }
}