using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Contracts.Dao;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Data.DataAccessObjects; 

internal class ItemDao : IItemDao
{ 
    private readonly AppDbContext _context; 
    
    public ItemDao(AppDbContext context) 
    { 
        _context = context; 
    }

    public async Task<ItemEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task RegisterAsync(ItemEntity item) 
    {
        await _context.Items.AddAsync(item); 
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ItemEntity item) 
    {
        _context.Remove(item); 
        await _context.SaveChangesAsync(); 
    }

    public async Task UpdateAsync(ItemEntity item) 
    { 
        _context.Update(item);
        await _context.SaveChangesAsync();
    } 
}