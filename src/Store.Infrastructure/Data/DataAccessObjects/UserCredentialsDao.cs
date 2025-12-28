using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Infrastructure.Data.DataAccessObjects;

internal sealed class UserCredentialsDao(AppDbContext context) : IUserCredentialsDao
{
    public async Task RegisterAsync(UserCredentialsEntity entity)
    {
        await context.UserCredentials.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public Task DeleteAsync(UserCredentialsEntity entity) =>
        context.UserCredentials
            .Where(u => u.Id == entity.Id)
            .ExecuteDeleteAsync();

    public Task<bool> IsExistsAsync(Guid id) =>
        context.UserCredentials
            .AsNoTracking()
            .AnyAsync(c => c.Id == id);

    public Task<UserCredentialsEntity?> GetByIdAsync(Guid id) =>
        context.UserCredentials
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

    public Task UpdateAsync(UserCredentialsEntity entity) =>
        context.UserCredentials
        .Where(u => u.Id == entity.Id)
        .ExecuteUpdateAsync(u => u
            .SetProperty(u => u.Role, entity.Role)
            .SetProperty(u => u.PasswordHash, entity.PasswordHash));
}
