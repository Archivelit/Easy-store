using Microsoft.EntityFrameworkCore;
using Store.Core.Contracts.Items;
using Store.Core.Contracts.Models;
using Store.Core.Contracts.Repositories;
using Store.Core.Exceptions.InvalidData.Item;
using Store.Core.Factories;
using Store.Core.Models;
using Store.Infrastructure.Contracts;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Data.Postgres;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;
    private readonly IItemFactory _itemFactory;
    private readonly IItemEntityFactory _itemEntityFactory;
    
    public ItemRepository(AppDbContext context, IItemFactory itemFactory, IItemEntityFactory itemEntityFactory)
    {
        _context = context;
        _itemFactory = itemFactory;
        _itemEntityFactory = itemEntityFactory;
    }

    public async Task<Item> GetItemByIdAsync(Guid id) => _itemFactory.Create(
            await _context.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id) 
        ?? throw new ItemNotFound($"Item with id {id} not found."));

    public async Task RegisterItemAsync(IItem item)
    {
        var entity = item is ItemEntity itemEntity
            ? itemEntity
            : _itemEntityFactory.Create(item);
        
        await _context.Items.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteItemByIdAsync(Guid id)
    {
        _context.Remove(await _context.Items.FirstOrDefaultAsync(i => i.Id == id));

        await _context.SaveChangesAsync();
    }
}