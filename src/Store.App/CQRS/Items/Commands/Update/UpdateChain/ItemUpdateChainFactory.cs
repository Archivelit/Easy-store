namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

/// <summary>
/// Factory for creating item update chain. Update the <see cref="Create"/> method to update the chain logic.
/// </summary>
public class ItemUpdateChainFactory : IItemUpdateChainFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ItemUpdateChainFactory> _logger;

    public ItemUpdateChainFactory(IServiceProvider serviceProvider, ILogger<ItemUpdateChainFactory> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <summary>
    /// Method for creating item update chain. 
    /// Update this method to update the chain logic.
    /// </summary>
    /// <returns>
    /// Item update chain
    /// </returns>
    public ItemUpdateChainBase Create()
    {
        _logger.LogDebug("Creating item update chain");

        var updateTitle = _serviceProvider.GetRequiredService<UpdateTitle>();
        var updateDescription = _serviceProvider.GetRequiredService<UpdateDescription>();
        var updatePrice = _serviceProvider.GetRequiredService<UpdatePrice>();
        var updateQuantity = _serviceProvider.GetRequiredService<UpdateQuantity>();
        var refreshUpdatedAt = _serviceProvider.GetRequiredService<RefreshUpdatedAt>();

        updateTitle
            .SetNext(updateDescription)
            .SetNext(updatePrice)
            .SetNext(updateQuantity)
            .SetNext(refreshUpdatedAt); // Don't remove, it should be in the chain for proper update time tracking

        _logger.LogDebug("Item update chain created");

        return updateTitle;
    }
}