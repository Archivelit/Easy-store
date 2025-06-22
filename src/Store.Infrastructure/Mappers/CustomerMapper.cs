using Store.Core.Models;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Mappers;

public static class CustomerMapper
{
    public static Customer ToDomain(CustomerEntity e) => new (e.Id, e.Name, e.Email, e.SubscriptionType);
    public static CustomerEntity ToEntity(Customer c, string passwordHash) => new (c.Id, c.Name, c.Email, c.SubscriptionType, passwordHash);
}