using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.App.GraphQl.Items;
using Store.App.GraphQl.Models;
using Store.Core.Contracts.Repositories;
using Store.Core.Exceptions.InvalidData.Item;
using Store.Core.Models;
using Store.Core.Models.Dto.Items;
using Store.Infrastructure.Contracts;
using Store.Infrastructure.Entities;
namespace Store.Infrastructure.Data.Postgres; 
public class ItemRepository : IItemRepository 
{ 
    private readonly AppDbContext _context; 
    private readonly IItemFactory _itemFactory; 
    private readonly IItemEntityFactory _itemEntityFactory; 
    private readonly ILogger<ItemRepository> _logger; 
    
    public ItemRepository(AppDbContext context, IItemFactory itemFactory, IItemEntityFactory itemEntityFactory, ILogger<ItemRepository> logger) 
    { 
        _context = context; 
        _itemFactory = itemFactory; 
        _itemEntityFactory = itemEntityFactory; 
        _logger = logger; 
    } 
    
    public async Task<Item> GetByIdAsync(Guid id) 
    { 
        var item = _itemFactory
            .Create(await _context.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id) 
            ?? throw new ItemNotFound($"Item with id {id} not found."));
        _logger.LogDebug("Item instance was created by id {id}", id); return item; 
    } 
    
    public async Task RegisterAsync(IItem item) 
    { 
        var entity = item is ItemEntity itemEntity 
            ? itemEntity 
            : _itemEntityFactory.Create(item);
        await _context.Items.AddAsync(entity); 
        await _context.SaveChangesAsync();
        _logger.LogInformation("Item {ItemId} succesfuly registered", item.Id);
    }

    public async Task DeleteByIdAsync(Guid id) 
    { 
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id); 
        if (item != null) 
        { 
            _context.Remove(item); 
            await _context.SaveChangesAsync(); 
            _logger.LogInformation("Item {ItemId} was succesfuly deleted", id); 
        } 
        ArgumentNullException.ThrowIfNull(item); 
    }

    public async Task UpdateAsync(ItemDto itemDto) 
    { 
        _context.Update(itemDto); 
        await _context.SaveChangesAsync();
        _logger.LogInformation("Item {ItemId} was succesfuly updated", itemDto.Id); 
    } 
}