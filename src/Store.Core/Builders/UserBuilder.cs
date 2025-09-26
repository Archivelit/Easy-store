namespace Store.Core.Builders;

#nullable disable
public class UserBuilder
{
    private Guid Id { get; set; }
    private string Name { get; set; }
    private string Email { get; set; }
    private Subscription SubscriptionType { get; set; }

    public UserBuilder() => InitDefault();

    /// <summary>
    /// Reset the builder properties.
    /// </summary>
    public UserBuilder Reset() => InitDefault();

    /// <summary>
    /// Set the builder based on item in param.
    /// </summary>
    public UserBuilder From(IUser user)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
        SubscriptionType = user.SubscriptionType;

        return this;
    }

    public UserBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }
    
    public UserBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public UserBuilder WithSubscriptionType(Subscription subscriptionType)
    {
        SubscriptionType = subscriptionType;
        return this;
    }
    
    /// <summary>
    /// "Seed" the builder fields with some parameters. Not recomended to use in core logic, made for tests only.
    /// </summary>
    public UserBuilder WithDefault()
    {
        return this
            .WithEmail("example@domain.com")
            .WithId(Guid.NewGuid())
            .WithName("Foo")
            .WithSubscriptionType(Subscription.None);
    }

    public User Build()
    {
        return new(Id, Name, Email, SubscriptionType);
    }

    /// <summary>
    /// Initialize builders defaults. Used to reset builder properties.
    /// </summary>
    private UserBuilder InitDefault()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Email = string.Empty;
        SubscriptionType = Subscription.None;

        return this;
    }

}