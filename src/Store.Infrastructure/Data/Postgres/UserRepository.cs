using Store.Core.Exceptions;
using Store.Core.Contracts.Repositories;
using Store.Core.Models;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Mappers;
using Microsoft.Extensions.Logging;

namespace Store.Infrastructure.Data.Postgres;

public class UserRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(AppDbContext db, ILogger<UserRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task RegisterAsync(User user, string passwordHash)
    {
        _logger.LogInformation("Registering user {UserId}", user.Id);

        await _db.Users.AddAsync(UserMapper.ToEntity(user, passwordHash));
        await _db.SaveChangesAsync();

        _logger.LogInformation("User {UserId} registered succesfuly", user.Id);
    }

    public async Task<bool> IsEmailClaimedAsync(string email) =>
        await _db.Users
            .AsNoTracking()
            .AnyAsync(c => c.Email == email);

    public async Task<(User user, string passwordHash)> GetByEmailAsync(string email)
    {
        var userEntity = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Email == email)
            ?? throw new CustomerNotFound($"User with email {email} not found");
        return (UserMapper.ToDomain(userEntity), userEntity.PasswordHash);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _db.Users.FindAsync(id)
            ?? throw new CustomerNotFound($"User with id {id} not found");

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> IsExistsAsync(Guid id) => await _db.Users.AsNoTracking().AnyAsync(c => c.Id == id);

    public async Task<(User user, string passwordHash)> GetByIdAsync(Guid id)
    {
        var entity = await _db.Users.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        
        if (entity == null)
            throw new CustomerNotFound($"User with id {id} not found");
        
        return (UserMapper.ToDomain(entity), entity.PasswordHash);
    }
    
    public async Task UpdateAsync(User user, string passwordHash)
    {
        var entity = UserMapper.ToEntity(user, passwordHash);
        
        _db.Users.Update(entity);
        await _db.SaveChangesAsync();
    }
}