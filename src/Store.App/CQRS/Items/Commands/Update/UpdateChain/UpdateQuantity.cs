using Store.Core.Builders;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateQuantity : ItemUpdateChainBase
{
    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.QuantityInStock != null)
        {
            _validator.ValidateQuantity((int)itemDto.QuantityInStock);
            builder.WithQuantityInStock((int)itemDto.QuantityInStock);
        }
        return base.Update(builder, itemDto);
    }
}