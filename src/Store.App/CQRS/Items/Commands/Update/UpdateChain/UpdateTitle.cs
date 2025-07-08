using Store.Core.Builders;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateTitle : ItemUpdateChainBase
{
    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Title != null)
        {
            _validator.ValidateTitle(itemDto.Title);
            builder.WithTitle(itemDto.Title);
        }
        return base.Update(builder, itemDto);
    }
}