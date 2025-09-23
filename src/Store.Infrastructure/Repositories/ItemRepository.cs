namespace Store.Infrastructure.Repositories;

public class ItemRepository(
    ILogger<ItemRepository> logger, 
    IItemFactory itemFactory, 
    IItemDao itemDao, 
    IDistributedCache cache, 
    IItemEntityFactory itemEntityFactory
    ) : IItemRepository
{
    private readonly ILogger<ItemRepository> _logger = logger;
    private readonly IItemFactory _itemFactory = itemFactory;
    private readonly IItemEntityFactory _itemEntityFactory = itemEntityFactory;
    private readonly IItemDao _itemDao = itemDao;
    private readonly IDistributedCache _cache = cache;

    public async Task DeleteByIdAsync(Guid id)
    {
        _logger.LogDebug("Deleting item {ItemId}", id);

        var key = $"item:{id}";

        await _itemDao.DeleteAsync(await _itemDao.GetByIdAsync(id) 
            ?? throw new InvalidItemDataException($"Item with id {id} not found."));
        await _cache.RemoveAsync(key);

        _logger.LogInformation("Item {ItemId} was successfully deleted", id);
    }

    public async Task<Item> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Requested to get item {ItemId}", id);
        
        var key = $"item:{id}";
        var itemFromCache = await _cache.GetStringAsync(key);
        
        if (itemFromCache is not null)
        {
            return _itemFactory.Create(JsonSerializer.Deserialize<ItemEntity>(itemFromCache)!);
        }

        var item = await _itemDao.GetByIdAsync(id)
            ?? throw new InvalidItemDataException($"Item with id {id} not found.");
        
        var json = JsonSerializer.Serialize(item);
        await _cache.SetStringAsync(key, json);

        return _itemFactory.Create(item);
    }

    public async Task RegisterAsync(IItem item)
    {
        var key = $"item:{item.Id}";

        await _itemDao.RegisterAsync(_itemEntityFactory.Create(item));
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(item));

        _logger.LogInformation("Item {ItemId} successfully registered", item.Id);
    }

    public async Task UpdateAsync(IItem item)
    {
        var key = $"user:{item.Id}";

        var entity = _itemEntityFactory.Create(item);

        await _itemDao.UpdateAsync(_itemEntityFactory.Create(entity));
        await _cache.UpdateCacheAsync(key, JsonSerializer.Serialize(entity));

        _logger.LogInformation("Item {ItemId} was successfully updated", item.Id);
    }
}