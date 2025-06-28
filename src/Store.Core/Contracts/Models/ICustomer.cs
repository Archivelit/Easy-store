using Store.Core.Enums.Subscriptions;

namespace Store.Core.Contracts.Models;

public interface ICustomer
{
    Guid Id { get;  }
    string Name { get;  }
    string Email { get;  }
    Subscription SubscriptionType { get;  }
}