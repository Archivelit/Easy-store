using Store.Core.Builders;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdatePrice : ItemUpdateChainBase
{
    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Price != null)
        {
            _validator.ValidatePrice((decimal)itemDto.Price);
            builder.WithPrice((decimal)itemDto.Price);
        }
        return base.Update(builder, itemDto);
    }
}