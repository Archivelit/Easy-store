namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateDescription : ItemUpdateChainBase
{
    private readonly IValidator<string?> _itemDescriptionValidator;
    
    public UpdateDescription(ILogger<UpdateDescription> logger,
        [FromKeyedServices(KeyedServicesKeys.ItemDescriptionValidator)] IValidator<string?> itemDescriptionValidator) : base(logger) 
    {
        _itemDescriptionValidator = itemDescriptionValidator;
    }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Description != null)
        {
            _logger.LogInformation("Updating description of {ItemId}", itemDto.Id);
            
            _itemDescriptionValidator.ValidateAndThrow(itemDto.Description);

            builder.WithDescription(itemDto.Description);
        }
        return base.Update(builder, itemDto);
    }
}