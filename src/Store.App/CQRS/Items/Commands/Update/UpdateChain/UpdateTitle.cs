using Microsoft.Extensions.Logging;
using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateTitle : ItemUpdateChainBase
{
    public UpdateTitle(IItemValidator validator, ILogger logger) : base(validator, logger) { }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Title != null)
        {
            _logger.LogDebug("Updating title of {ItemId}", itemDto.Id);
            _validator.ValidateTitle(itemDto.Title);
            builder.WithTitle(itemDto.Title);
        }
        return base.Update(builder, itemDto);
    }
}