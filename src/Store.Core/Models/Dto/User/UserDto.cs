namespace Store.Core.Models.Dto.User;

public record UserDto : IUser
{
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required] public string Name { get; init; }
    [Required] public string Email { get; init; }
    public Subscription SubscriptionType { get; init; } = Subscription.None;
    
    public UserDto(Models.User user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
        SubscriptionType = user.SubscriptionType;
    }
}