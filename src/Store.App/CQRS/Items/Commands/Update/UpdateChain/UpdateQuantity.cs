namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateQuantity : ItemUpdateChainBase
{
    public UpdateQuantity(ILogger<UpdateQuantity> logger) : base(logger) { }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.QuantityInStock != null)
        {
            _logger.LogDebug("Updating quantity of {ItemId}", itemDto.QuantityInStock);
            new QuantityValidator().Validate((int)itemDto.QuantityInStock, options =>
            {
                options.ThrowOnFailures();
            });
            builder.WithQuantityInStock((int)itemDto.QuantityInStock);
        }
        return base.Update(builder, itemDto);
    }
}