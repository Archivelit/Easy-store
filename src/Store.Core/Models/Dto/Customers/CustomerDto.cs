using System.ComponentModel.DataAnnotations;
using Store.Core.Contracts.Models;
using Store.Core.Enums.Subscriptions;

namespace Store.Core.Models.Dto.Customers;

public record CustomerDto : ICustomer
{
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required] public string Name { get; init; }
    [Required] public string Email { get; init; }
    public Subscription SubscriptionType { get; init; } = Subscription.None;

    public CustomerDto(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public CustomerDto(Customer customer)
    {
        Id = customer.Id;
        Name = customer.Name;
        Email = customer.Email;
        SubscriptionType = customer.SubscriptionType;
    }
}