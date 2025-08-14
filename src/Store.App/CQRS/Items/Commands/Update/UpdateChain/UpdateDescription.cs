using Microsoft.Extensions.Logging;
using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateDescription : ItemUpdateChainBase
{
    public UpdateDescription(IItemValidator validator, ILogger logger) : base(validator, logger) { }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Description != null)
        {
            _logger.LogInformation("Updating description of {ItemId}", itemDto.Id);
            _validator.ValidateTitle(itemDto.Description);
            builder.WithDescription(itemDto.Description);
        }
        return base.Update(builder, itemDto);
    }
}