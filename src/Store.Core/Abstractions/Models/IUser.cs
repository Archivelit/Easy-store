namespace Store.Core.Abstractions.Models;

public interface IUser
{
    Guid Id { get; }
    string Name { get; }
    string Email { get; }
    Subscription SubscriptionType { get; }
}