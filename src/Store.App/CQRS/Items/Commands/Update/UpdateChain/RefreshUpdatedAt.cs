namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

/// <summary>
/// This element of the chain refreshes the UpdatedAt property to the current UTC time. 
/// </summary>
public class RefreshUpdatedAt : ItemUpdateChainBase
{
    public RefreshUpdatedAt(ILogger<RefreshUpdatedAt> logger) : base(logger) { }

    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        _logger.LogInformation("Refreshing update date of {ItemId}", itemDto.Id);
        builder.WithUpdatedAt(DateTime.UtcNow);
        return base.Update(builder, itemDto);
    }
}