using Store.Core.Enums.Subscriptions;

namespace Store.Core.Contracts.Models;

public interface IUser
{
    Guid Id { get;  }
    string Name { get;  }
    string Email { get;  }
    Subscription SubscriptionType { get;  }
}