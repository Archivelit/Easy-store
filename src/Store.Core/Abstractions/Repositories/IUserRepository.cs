namespace Store.Core.Abstractions.Repositories;

/// <summary>
/// Recomended for work with db.
/// <para>
/// Operates with <see cref="IItemDao"> and IDistributed cache implementations.
/// </para>
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Save user to cache and database. 
    /// </summary>
    Task RegisterAsync(User user);
    /// <summary>
    /// Delete user from cache and database. Throw <see cref="InvalidUserDataException"> if user not registered in database.
    /// </summary>
    Task DeleteAsync(Guid id);
    /// <summary>
    /// Check users with specified email.
    /// </summary>
    Task<bool> IsEmailClaimedAsync(string email);
    /// <summary>
    /// Search user with specified id in cache and database. 
    /// </summary>
    Task<bool> IsExistsAsync(Guid id);
    /// <summary>
    /// Check user in cache, then check the database.<br/>
    /// Store user in cache if was not stored.
    /// </summary>
    /// <returns>
    /// User with specified id form database or chache. If no user found throws <see cref="InvalidUserDataException">.
    /// <returns/>
    Task<User> GetByIdAsync(Guid id);
    /// <summary>
    /// Update user model in cache and database.
    /// </summary>
    /// <param name="item">
    /// Complete user entity model. Must be based on model from database for correct update.
    /// </param>
    Task UpdateAsync(User user);
}