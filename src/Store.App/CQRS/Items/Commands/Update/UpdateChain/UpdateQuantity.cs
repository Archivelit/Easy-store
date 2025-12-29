namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateQuantity : ItemUpdateChainBase
{
    private readonly IValidator<int> _itemQuantityValidator;

    public UpdateQuantity(ILogger<UpdateQuantity> logger,
        [FromKeyedServices(KeyedServicesKeys.ItemQuantityValidator)] IValidator<int> itemQuantityValidator) 
        : base(logger) 
    {
        _itemQuantityValidator = itemQuantityValidator;
    }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.QuantityInStock != null)
        {
            _logger.LogInformation("Updating quantity of {ItemId}", itemDto.QuantityInStock);
            
            _itemQuantityValidator.ValidateAndThrow((int)itemDto.QuantityInStock);

            builder.WithQuantityInStock((int)itemDto.QuantityInStock);
        }
        return base.Update(builder, itemDto);
    }
}