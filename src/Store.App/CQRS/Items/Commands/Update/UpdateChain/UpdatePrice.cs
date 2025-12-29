namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdatePrice : ItemUpdateChainBase
{
    private readonly IValidator<decimal> _itemPriceValidator;

    public UpdatePrice(ILogger<UpdatePrice> logger, 
        [FromKeyedServices(KeyedServicesKeys.ItemPriceValidator)] IValidator<decimal> itemPriceValidator) 
        : base(logger) 
    {
        _itemPriceValidator = itemPriceValidator;
    }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Price != null)
        {
            _logger.LogInformation("Updating price of item {ItemId}", itemDto.Id);
            
            _itemPriceValidator.ValidateAndThrow((decimal)itemDto.Price);
            
            builder.WithPrice((decimal)itemDto.Price);
        }
        return base.Update(builder, itemDto);
    }
}