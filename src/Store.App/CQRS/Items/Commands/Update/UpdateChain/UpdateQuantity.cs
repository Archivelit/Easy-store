using Microsoft.Extensions.Logging;
using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateQuantity : ItemUpdateChainBase
{
    public UpdateQuantity(IItemValidator validator, ILogger logger) : base(validator, logger) { }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.QuantityInStock != null)
        {
            _logger.LogDebug("Updating quantity of {ItemId}", itemDto.QuantityInStock);
            _validator.ValidateQuantity((int)itemDto.QuantityInStock);
            builder.WithQuantityInStock((int)itemDto.QuantityInStock);
        }
        return base.Update(builder, itemDto);
    }
}