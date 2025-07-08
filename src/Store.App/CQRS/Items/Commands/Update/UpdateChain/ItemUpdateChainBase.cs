using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class ItemUpdateChainBase : IItemUpdateChain
{
    protected IItemUpdateChain _next;
    protected IItemValidator _validator;

    public IItemUpdateChain SetNext(IItemUpdateChain next)
    {
        _next = next;
        return next;
    }

    public virtual ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        return _next.Update(builder, itemDto);
    }
}