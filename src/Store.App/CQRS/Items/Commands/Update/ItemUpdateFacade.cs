namespace Store.App.CQRS.Items.Commands.Update;

public sealed class ItemUpdateFacade
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

    /// <summary>
    /// Updates item in the system with provided data. <br/>
    /// The model is based on the existing item in the database. 
    /// Whole logic of changing model is incapsulated in the chain. 
    /// See more in <see cref="ItemUpdateChainFactory"/>
    /// </summary>
    /// <returns>
    /// Updated item model
    /// </returns>
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

    /// <summary>
    /// Gets new item based on db model with new data from dto
    /// </summary>
    /// <param name="builder">Item builder</param>
    /// <param name="itemDto">Data for update</param>
    /// <returns>Item based on db model with new data</returns>
    private Item GetNewItem(ItemBuilder builder, UpdateItemDto itemDto)
    {
        builder = _chain.Update(builder, itemDto);
        
        var item = builder.Build();
        
        return item;
    }
}