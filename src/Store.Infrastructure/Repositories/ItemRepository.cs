using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Store.Core.Contracts.Factories;
using Store.Core.Contracts.Repositories;
using Store.Core.Exceptions.InvalidData;
using Store.Core.Models;
using Store.Infrastructure.Contracts;
using Store.Infrastructure.Data.DataAccessObjects;
using Store.Infrastructure.Extensions;
using System.Text.Json;

namespace Store.Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly ILogger<ItemRepository> _logger;
    private readonly IItemFactory _itemFactory;
    private readonly IItemEntityFactory _itemEntityFactory;
    private readonly IItemDao _itemDao;
    private readonly IDistributedCache _cache;

    public ItemRepository(ILogger<ItemRepository> logger, IItemFactory itemFactory, IItemDao itemDao, IDistributedCache cache, IItemEntityFactory itemEntityFactory)
    {
        _logger = logger;
        _itemFactory = itemFactory;
        _itemDao = itemDao;
        _cache = cache;
        _itemEntityFactory = itemEntityFactory;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        _logger.LogDebug("Deleting item {ItemId}", id);

        var key = $"item:{id}";

        await _itemDao.DeleteAsync(await _itemDao.GetByIdAsync(id) 
            ?? throw new InvalidItemDataException($"Item with id {id} not found."));
        await _cache.RemoveAsync(key);

        _logger.LogInformation("Item {ItemId} was succesfuly deleted", id);
    }

    public async Task<Item> GetByIdAsync(Guid id)
    {
        _logger.LogDebug("Requested to get item {ItemId}", id);
        var item = await _itemDao.GetByIdAsync(id)
            ?? throw new InvalidItemDataException($"Item with id {id} not found.");
        
        return _itemFactory.Create(item);
    }

    public async Task RegisterAsync(IItem item)
    {
        var key = $"item:{item.Id}";

        await _itemDao.RegisterAsync(_itemEntityFactory.Create(item));
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(item));

        _logger.LogInformation("Item {ItemId} succesfuly registered", item.Id);
    }

    public async Task UpdateAsync(IItem item)
    {
        var key = $"user:{item.Id}";

        var entity = _itemEntityFactory.Create(item);

        await _itemDao.UpdateAsync(_itemEntityFactory.Create(entity));
        await _cache.UpdateCacheAsync(key, JsonSerializer.Serialize(entity));

        _logger.LogInformation("Item {ItemId} was succesfuly updated", item.Id);
    }
}