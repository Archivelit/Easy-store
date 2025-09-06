namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class ItemUpdateChainFactory : IItemUpdateChainFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ItemUpdateChainFactory> _logger;

    public ItemUpdateChainFactory(IServiceProvider serviceProvider, ILogger<ItemUpdateChainFactory> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public ItemUpdateChainBase Create()
    {
        _logger.LogDebug("Creating item update chain");

        var updateTitle = _serviceProvider.GetRequiredService<UpdateTitle>();
        var updateDescription = _serviceProvider.GetRequiredService<UpdateDescription>();
        var updatePrice = _serviceProvider.GetRequiredService<UpdatePrice>();
        var updateQuantity = _serviceProvider.GetRequiredService<UpdateQuantity>();
        var refreshUpdatedAt = _serviceProvider.GetRequiredService<RefreshUpdatedAt>();
        
        updateTitle.SetNext(updateDescription).SetNext(updatePrice).SetNext(updateQuantity).SetNext(refreshUpdatedAt);

        _logger.LogDebug("Item update chain created");

        return updateTitle;
    }
}