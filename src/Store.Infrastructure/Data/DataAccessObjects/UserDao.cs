using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Data.DataAccessObjects;

public interface IUserDao
{
    Task RegisterAsync(UserEntity entity);
    Task<bool> IsEmailClaimedAsync(string email);
    Task<UserEntity?> GetByEmailAsync(string email);
    Task DeleteAsync(UserEntity entity);
    Task<bool> IsExistsAsync(Guid id);
    Task<UserEntity?> GetByIdAsync(Guid id);
    Task UpdateAsync(UserEntity entity);
}


internal class UserDao : IUserDao
{
    private readonly AppDbContext _db;

    public UserDao(AppDbContext db)
    {
        _db = db;
    }

    public async Task RegisterAsync(UserEntity entity)
    {
        await _db.Users.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> IsEmailClaimedAsync(string email) =>
        await _db.Users
            .AsNoTracking()
            .AnyAsync(c => c.Email == email);

    public async Task<UserEntity?> GetByEmailAsync(string email) => await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email);

    public async Task DeleteAsync(UserEntity entity)
    {
        _db.Users.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> IsExistsAsync(Guid id) => await _db.Users
        .AsNoTracking()
        .AnyAsync(c => c.Id == id);

    public async Task<UserEntity?> GetByIdAsync(Guid id) => await _db.Users
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Id == id);
    
    public async Task UpdateAsync(UserEntity entity)
    {   
        _db.Users.Update(entity);
        await _db.SaveChangesAsync();
    }
}