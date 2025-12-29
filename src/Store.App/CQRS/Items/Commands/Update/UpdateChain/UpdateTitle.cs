namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateTitle : ItemUpdateChainBase
{
    private readonly IValidator<string> _itemTitleValidator;
    public UpdateTitle(ILogger<UpdateTitle> logger, 
        [FromKeyedServices(KeyedServicesKeys.ItemTitleValidator)] IValidator<string> itemTitleValidator) : base(logger)
    {
        _itemTitleValidator = itemTitleValidator;
    }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Title != null)
        {
            _logger.LogInformation("Updating title of {ItemId}", itemDto.Id);
            
            _itemTitleValidator.ValidateAndThrow(itemDto.Title);
            
            builder.WithTitle(itemDto.Title);
        }
        return base.Update(builder, itemDto);
    }
}