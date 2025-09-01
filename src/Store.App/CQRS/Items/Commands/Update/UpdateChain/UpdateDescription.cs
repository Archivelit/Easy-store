using FluentValidation;
using Microsoft.Extensions.Logging;
using Store.Core.Builders;
using Store.Core.Models.Dto.Items;
using Store.Core.Utils.Validators;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateDescription : ItemUpdateChainBase
{
    public UpdateDescription(ILogger<UpdateDescription> logger) : base(logger) { }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Description != null)
        {
            _logger.LogInformation("Updating description of {ItemId}", itemDto.Id);
            new DescriptionValidator().Validate(itemDto.Description, options =>
            {
                options.ThrowOnFailures();
            });
            builder.WithDescription(itemDto.Description);
        }
        return base.Update(builder, itemDto);
    }
}