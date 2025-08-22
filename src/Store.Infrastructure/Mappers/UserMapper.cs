using Store.Core.Models;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Mappers;

public static class UserMapper
{
    public static User ToDomain(UserEntity e) => new (e.Id, e.Name, e.Email, e.SubscriptionType);
    public static UserEntity ToEntity(User u) => new (u.Id, u.Name, u.Email, u.SubscriptionType);
}