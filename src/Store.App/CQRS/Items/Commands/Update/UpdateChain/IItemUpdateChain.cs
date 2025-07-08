using Store.Core.Builders;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public interface IItemUpdateChain
{
    IItemUpdateChain SetNext(IItemUpdateChain next);
    ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto);
}