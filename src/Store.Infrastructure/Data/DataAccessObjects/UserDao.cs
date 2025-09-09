namespace Store.Infrastructure.Data.DataAccessObjects;

internal class UserDao(AppDbContext context) : IUserDao
{
    public async Task RegisterAsync(UserEntity entity)
    {
        await context.Users.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<bool> IsEmailClaimedAsync(string email) =>
        await context.Users
            .AsNoTracking()
            .AnyAsync(c => c.Email == email);

    public async Task<UserEntity?> GetByEmailAsync(string email) => await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email);

    public async Task DeleteAsync(UserEntity entity)
    {
        context.Users.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<bool> IsExistsAsync(Guid id) => await context.Users
        .AsNoTracking()
        .AnyAsync(c => c.Id == id);

    public async Task<UserEntity?> GetByIdAsync(Guid id) => await context.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Id == id);
    
    public async Task UpdateAsync(UserEntity entity)
    {   
        context.Users.Update(entity);
        await context.SaveChangesAsync();
    }
}