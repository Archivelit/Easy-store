using Microsoft.Extensions.DependencyInjection;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class ItemUpdateChainFactory : IItemUpdateChainFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ItemUpdateChainFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ItemUpdateChainBase Create()
    {
        var updateTitle = _serviceProvider.GetRequiredService<UpdateTitle>();
        var updateDescription = _serviceProvider.GetRequiredService<UpdateDescription>();
        var updatePrice = _serviceProvider.GetRequiredService<UpdatePrice>();
        var updateQuantity = _serviceProvider.GetRequiredService<UpdateQuantity>();
        var refreshUpdatedAt = _serviceProvider.GetRequiredService<RefreshUpdatedAt>();
        
        updateTitle.SetNext(updateDescription).SetNext(updatePrice).SetNext(updateQuantity).SetNext(refreshUpdatedAt);
        
        return updateTitle;
    }
}