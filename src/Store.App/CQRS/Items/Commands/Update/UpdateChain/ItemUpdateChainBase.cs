namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

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

    public virtual ItemBuilder Update(ItemBuilder builder, UpdateItemDto item)
    {
        if (_next != null) 
            return _next.Update(builder, item);
        return builder;
    }
}