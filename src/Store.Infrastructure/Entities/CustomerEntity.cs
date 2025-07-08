using System.ComponentModel.DataAnnotations;
using Store.App.GraphQl.Models;
using Store.Core.Enums.Subscriptions;

namespace Store.Infrastructure.Entities;

public class CustomerEntity : ICustomer
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

    [Required]
    public string PasswordHash { get; private set; }
    
    public CustomerEntity(Guid id, string name, string email, Subscription subscriptionType, string passwordHash)
    {
        Id = id;
        Name = name;
        Email = email;
        SubscriptionType = subscriptionType;
        PasswordHash = passwordHash;
    }
    
    private CustomerEntity() { }
}