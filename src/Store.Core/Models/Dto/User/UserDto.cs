namespace Store.Core.Models.Dto.User;

public record UserDto : IUser
{
    
    public Guid Id { get; init; }
    [Required] public string Name { get; init; }
    [Required] public string Email { get; init; }
    public Subscription SubscriptionType { get; init; }
    
    public UserDto(IUser user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
        SubscriptionType = user.SubscriptionType;
    }

    public UserDto(Guid id, string name, string email, Subscription subscriptionType)
    {
        Id = id;
        Name = name;
        Email = email;
        SubscriptionType = subscriptionType;
    }
}