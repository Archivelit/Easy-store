using Store.Core.Builders;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateDescription : ItemUpdateChainBase
{
    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Description != null)
        {
            _validator.ValidateTitle(itemDto.Description);
            builder.WithDescription(itemDto.Description);
        }
        return base.Update(builder, itemDto);
    }
}