namespace Store.Core.Models.Entities;

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

    [JsonConstructor]
    public UserEntity(Guid id, string name, string email, Subscription subscriptionType)
    {
        Id = id;
        Name = name;
        Email = email;
        SubscriptionType = subscriptionType;
    }

    public UserEntity(IUser user) : this(user.Id, user.Name, user.Email, user.SubscriptionType) { }
    
    // For ef core only
#pragma warning disable CS8618
    public UserEntity() { }
}