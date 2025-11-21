namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

/// <summary>
/// Base class for item update chain. Inherit from this class to create new chain link and add to <see cref="ItemUpdateChainFactory.Create"/> method to add own logic.
/// </summary>
public class ItemUpdateChainBase : IItemUpdateChain
{
    protected IItemUpdateChain? _next;
    protected ILogger _logger;

    public ItemUpdateChainBase(ILogger<ItemUpdateChainBase> logger)
    {
        _logger = logger;
    }

    public IItemUpdateChain SetNext(IItemUpdateChain next)
    {
        _next = next;
        return next;
    }

    /// <summary>
    /// Executes update method of next element in the chain if not null.<br/>
    /// Use this method in inherited classes to call next element in the chain.
    /// </summary>
    /// <param name="builder">Item builder</param>
    /// <param name="itemDto">Item data for update</param>
    /// <returns>
    /// Builder with updated item model
    /// </returns>
    public virtual ItemBuilder Update(ItemBuilder builder, UpdateItemDto item)
    {
        if (_next != null) 
            return _next.Update(builder, item);
        return builder;
    }
}