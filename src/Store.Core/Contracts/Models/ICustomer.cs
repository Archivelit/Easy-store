using Store.Core.Enums.Subscriptions;

namespace Store.App.GraphQl.Models;

public interface ICustomer
{
    Guid Id { get;  }
    string Name { get;  }
    string Email { get;  }
    Subscription SubscriptionType { get;  }
}