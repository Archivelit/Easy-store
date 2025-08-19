using Microsoft.Extensions.Logging;
using Store.Core.Builders;
using Store.Core.Contracts.Validation;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdatePrice : ItemUpdateChainBase
{
    public UpdatePrice(IItemValidator validator, ILogger logger) : base(validator, logger) { }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Price != null)
        {
            _logger.LogInformation("Updating price of item {ItemId}", itemDto.Id);
            _validator.ValidatePrice((decimal)itemDto.Price);
            builder.WithPrice((decimal)itemDto.Price);
        }
        return base.Update(builder, itemDto);
    }
}