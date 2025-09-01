using FluentValidation;
using Microsoft.Extensions.Logging;
using Store.Core.Builders;
using Store.Core.Models.Dto.Items;
using Store.Core.Utils.Validators;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdatePrice : ItemUpdateChainBase
{
    public UpdatePrice(ILogger<UpdatePrice> logger) : base(logger) { }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Price != null)
        {
            _logger.LogInformation("Updating price of item {ItemId}", itemDto.Id);
            new PriceValidator().Validate((decimal)itemDto.Price, options =>
            {
                options.ThrowOnFailures();
            });
            builder.WithPrice((decimal)itemDto.Price);
        }
        return base.Update(builder, itemDto);
    }
}