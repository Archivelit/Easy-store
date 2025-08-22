using System.ComponentModel.DataAnnotations;
using Store.Core.Contracts.Models;
using Store.Core.Enums.Subscriptions;

namespace Store.Infrastructure.Entities;

public class UserEntity : IUser
{
    [Key]
    public Guid Id { get; private set; }

    [Required]
    [MaxLength(100)] 
    public string Name { get; private set; }

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; private set; } 

    [Required]
    public Subscription SubscriptionType { get; private set; }
    
    public UserEntity(Guid id, string name, string email, Subscription subscriptionType)
    {
        Id = id;
        Name = name;
        Email = email;
        SubscriptionType = subscriptionType;
    }
    
    private UserEntity() { }
}