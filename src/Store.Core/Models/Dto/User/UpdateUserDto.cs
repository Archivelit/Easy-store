namespace Store.Core.Models.Dto.User;

public record UpdateUserDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    public Subscription? SubscriptionType { get; init; }

    public UpdateUserDto(IUser user) : this(user.Id, user.Name, user.Email, user.SubscriptionType) { }

    public UpdateUserDto(Guid id, string? name, string? email, Subscription? subscriptionType)
    {
        Id = id;
        Name = name;
        Email = email;
        SubscriptionType = subscriptionType;
    }
}