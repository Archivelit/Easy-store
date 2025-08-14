using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Store.App.CQRS.Customers.Commands.Update.UpdateChain;

public class CustomerUpdateChainFactory : ICustomerUpdateChainFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CustomerUpdateChainFactory> _logger;

    public CustomerUpdateChainFactory(IServiceProvider serviceProvider, ILogger<CustomerUpdateChainFactory> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public ICustomerUpdateChain Create()
    {
        _logger.LogDebug("Creating customer update chain");

        var emailHandler = _serviceProvider.GetRequiredService<UpdateCustomerEmail>();
        var nameHandler = _serviceProvider.GetRequiredService<UpdateCustomerName>();
        var subscriptionHandler = _serviceProvider.GetRequiredService<UpdateCustomerSubscription>();

        emailHandler.SetNext(nameHandler).SetNext(subscriptionHandler).SetNext(null);

        _logger.LogDebug("Customer update chain created");

        return emailHandler;
    }
}
