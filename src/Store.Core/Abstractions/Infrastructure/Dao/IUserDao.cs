namespace Store.Core.Abstractions.Infrastructure.Dao;

/// <summary>
/// Implementation must operate with ef core. Needed to incapsulate ef core logic in repositories. 
/// </summary>
public interface IUserDao
{
    /// <summary>
    /// Creates user model in database
    /// <summary/>
    Task RegisterAsync(UserEntity entity);
    /// <summary>
    /// Check users with specified email. Return false if not found
    /// </summary>
    Task<bool> IsEmailClaimedAsync(string email);
    /// <returns>
    /// User with specified email
    /// </returns>
    Task<UserEntity?> GetByEmailAsync(string email);
    /// <summary>
    /// Delete user model from database.
    /// </summary>
    Task DeleteAsync(UserEntity entity);
    /// <summary>
    /// Search user with specified id in database.
    /// </summary>
    Task<bool> IsExistsAsync(Guid id);
    /// <returns>
    /// User with specified id
    /// <returns/>
    Task<UserEntity?> GetByIdAsync(Guid id);
    /// <summary>
    /// Update user model in database.<br/>
    /// Updates whole model based on entity from params.
    /// </summary>
    /// <param name="entity">
    /// Complete user entity model. Must be based on model in database for correct update.
    /// </param>
    Task UpdateAsync(UserEntity entity);
}