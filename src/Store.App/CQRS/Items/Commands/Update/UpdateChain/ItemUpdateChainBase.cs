using Microsoft.Extensions.Logging;
using Store.Core.Builders;
using Store.Core.Contracts.Validation;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class ItemUpdateChainBase : IItemUpdateChain
{
    protected IItemUpdateChain? _next;
    protected IItemValidator _validator;
    protected ILogger _logger;

    public ItemUpdateChainBase(IItemValidator validator, ILogger logger)
    {
        _validator = validator;
        _logger = logger;
    }

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