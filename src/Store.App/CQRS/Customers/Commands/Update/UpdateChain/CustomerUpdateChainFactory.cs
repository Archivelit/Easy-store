using Microsoft.Extensions.DependencyInjection;

namespace Store.App.CQRS.Customers.Commands.Update.UpdateChain;

public class CustomerUpdateChainFactory : ICustomerUpdateChainFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CustomerUpdateChainFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICustomerUpdateChain Create()
    {
        var emailHandler = _serviceProvider.GetRequiredService<UpdateCustomerEmail>();
        var nameHandler = _serviceProvider.GetRequiredService<UpdateCustomerName>();
        var subscriptionHandler = _serviceProvider.GetRequiredService<UpdateCustomerSubscription>();

        emailHandler.SetNext(nameHandler).SetNext(subscriptionHandler).SetNext(null);

        return emailHandler;
    }
}
