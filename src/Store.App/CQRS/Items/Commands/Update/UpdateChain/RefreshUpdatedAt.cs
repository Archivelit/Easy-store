using Microsoft.Extensions.Logging;
using Store.Core.Builders;
using Store.Core.Contracts.Validation;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class RefreshUpdatedAt : ItemUpdateChainBase
{
    public RefreshUpdatedAt(IItemValidator validator, ILogger logger) : base(validator, logger) { }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        _logger.LogInformation("Refreshing update date of {ItemId}", itemDto.Id);
        builder.WithUpdatedAt(DateTime.UtcNow);
        return base.Update(builder, itemDto);
    }
}