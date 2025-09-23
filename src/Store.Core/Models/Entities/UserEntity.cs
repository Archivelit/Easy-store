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
    [Required]
    public string ProfileImageUrl { get; private set; } = string.Empty;

    [JsonConstructor]
    public UserEntity(Guid id, string name, string email, Subscription subscriptionType, string profileImageUrl)
    {
        Id = id;
        Name = name;
        Email = email;
        SubscriptionType = subscriptionType;
        ProfileImageUrl = profileImageUrl;
    }

    public UserEntity(IUser user) : this(user.Id, user.Name, user.Email, user.SubscriptionType, user.ProfileImageUrl) { }
    
#pragma warning disable CS8618
    public UserEntity() { }
}