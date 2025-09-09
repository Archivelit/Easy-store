namespace Store.App.CQRS.Items.Commands.Update;

public class ItemUpdateFacade
{
    private readonly IItemUpdateChain _chain;
    private readonly IItemRepository _itemRepository;
    private readonly ILogger<ItemUpdateFacade> _logger;

    public ItemUpdateFacade(IItemUpdateChainFactory factory, IItemRepository itemRepository, ILogger<ItemUpdateFacade> logger)
    {
        _itemRepository = itemRepository;
        _chain = factory.Create();
        _logger = logger;
    }

    public async Task<ItemDto> UpdateItemAsync(UpdateItemDto item)
    {
        try
        {
            _logger.LogInformation("Starting update of {ItemId}", item.Id);

            var itemFromDb = await _itemRepository.GetByIdAsync(item.Id);

            var builder = new ItemBuilder();
            builder.From(itemFromDb);

            var updatedItem = GetNewItem(builder, item);

            await _itemRepository.UpdateAsync(updatedItem);

            _logger.LogInformation("Item {ItemId} updated succesfuly", item.Id);

            return new (updatedItem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during Updating Item");
            throw;
        }
    }

    private Item GetNewItem(ItemBuilder builder, UpdateItemDto itemDto)
    {
        builder = _chain.Update(builder, itemDto);
        
        var item = builder.Build();
        
        return item;
    }
}