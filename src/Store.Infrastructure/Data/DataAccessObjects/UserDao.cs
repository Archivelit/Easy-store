namespace Store.Infrastructure.Data.DataAccessObjects;

internal class UserDao(AppDbContext context) : IUserDao
{
    public async Task RegisterAsync(UserEntity entity)
    {
        await context.Users.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public Task<bool> IsEmailClaimedAsync(string email) =>
        context.Users
            .AsNoTracking()
            .AnyAsync(c => c.Email == email);

    public Task<UserEntity?> GetByEmailAsync(string email) =>
        context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email);

    public Task DeleteAsync(UserEntity entity) =>
        context.Users
            .Where(u => u.Id == entity.Id)
            .ExecuteDeleteAsync();

    public Task<bool> IsExistsAsync(Guid id) =>
        context.Users
            .AsNoTracking()
            .AnyAsync(c => c.Id == id);

    public Task<UserEntity?> GetByIdAsync(Guid id) =>
        context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

    public Task UpdateAsync(UserEntity entity) =>
        context.Users
        .Where(u => u.Id == entity.Id)
        .ExecuteUpdateAsync(u => u
            .SetProperty(u => u.Email, entity.Email)
            .SetProperty(u => u.SubscriptionType, entity.SubscriptionType)
            .SetProperty(u => u.Name, entity.Name));
}