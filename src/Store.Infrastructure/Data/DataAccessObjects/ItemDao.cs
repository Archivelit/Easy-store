namespace Store.Infrastructure.Data.DataAccessObjects; 

internal class ItemDao(AppDbContext context) : IItemDao
{
    public async Task<ItemEntity?> GetByIdAsync(Guid id)
    {
        return await context.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task RegisterAsync(ItemEntity item) 
    {
        await context.Items.AddAsync(item); 
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ItemEntity item) 
    {
        context.Remove(item); 
        await context.SaveChangesAsync(); 
    }

    public async Task UpdateAsync(ItemEntity item) 
    { 
        context.Update(item);
        await context.SaveChangesAsync();
    } 
}