namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public interface IItemUpdateChain
{
    /// <summary>
    /// Sets next element in the chain. <br/>
    /// Used for setting order of the elements in the <see cref="ItemUpdateChainFactory.Create"/> method.
    /// </summary>
    IItemUpdateChain SetNext(IItemUpdateChain next);
    
    ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto);
}