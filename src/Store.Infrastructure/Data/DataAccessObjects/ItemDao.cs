using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Data.DataAccessObjects; 

public interface IItemDao
{
    Task<ItemEntity?> GetByIdAsync(Guid id);
    Task RegisterAsync(ItemEntity item);
    Task UpdateAsync(ItemEntity item);
    Task DeleteAsync(ItemEntity item);
}

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