using System.ComponentModel.DataAnnotations;
using Store.Core.Enums.Subscriptions;

namespace Store.Infrastructure.Entities;

    public class CustomerEntity
    {
        [Key]
        public Guid Id { get; internal set; }

        [Required]
        [MaxLength(100)] 
        public string Name { get; internal set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; internal set; } 

        [Required]
        public Subscription SubscriptionType { get; internal set; }

        [Required]
        public string PasswordHash { get; internal set; }
        
        public CustomerEntity(Guid id, string name, string email, Subscription subscriptionType, string passwordHash)
        {
            Id = id;
            Name = name;
            Email = email;
            SubscriptionType = subscriptionType;
            PasswordHash = passwordHash;
        }
        
        internal CustomerEntity() { }
    }