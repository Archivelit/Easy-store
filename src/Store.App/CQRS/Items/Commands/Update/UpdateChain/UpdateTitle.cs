namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class UpdateTitle : ItemUpdateChainBase
{
    public UpdateTitle(ILogger<UpdateTitle> logger) : base(logger) { }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        if (itemDto.Title != null)
        {
            _logger.LogDebug("Updating title of {ItemId}", itemDto.Id);
            new TitleValidator().Validate(itemDto.Title, options =>
            {
                options.ThrowOnFailures();
            });
            builder.WithTitle(itemDto.Title);
        }
        return base.Update(builder, itemDto);
    }
}