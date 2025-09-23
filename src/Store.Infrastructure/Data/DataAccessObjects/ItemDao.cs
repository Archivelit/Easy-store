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
        await context.Items.Where(i => i.Id == item.Id).ExecuteDeleteAsync(); 
    }

    public async Task UpdateAsync(ItemEntity item) 
    { 
        await context.Items.Where(i => i.Id == item.Id).ExecuteUpdateAsync(i => 
            i.SetProperty(p => p.Title, item.Title)
            .SetProperty(p => p.Description, item.Description)
            .SetProperty(p => p.Price, item.Price)
            .SetProperty(p => p.QuantityInStock, item.QuantityInStock)
            .SetProperty(p => p.UpdatedAt, DateTime.UtcNow));
    } 
}