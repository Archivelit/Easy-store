namespace Store.Core.Abstractions.Infrastructure.Dao;

public interface IUserCredentialsDao
{
    /// <summary>
    /// Creates user credentials model in database.
    /// <summary/>
    Task RegisterAsync(UserCredentialsEntity entity);

    /// <summary>
    /// Deletes user credentials model from database.
    /// </summary>
    Task DeleteAsync(UserCredentialsEntity entity);
    
    /// <summary>
    /// Search user with specified id in database.
    /// </summary>
    Task<bool> IsExistsAsync(Guid id);

    /// <returns>
    /// User credentials with specified id.
    /// <returns/>
    Task<UserCredentialsEntity?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Update user model in database.<br/>
    /// Updates whole model based on entity from params.
    /// </summary>
    /// <param name="entity">
    /// Complete user entity model. Must be based on model in database for correct update.
    /// </param>
    Task UpdateAsync(UserCredentialsEntity entity);
}
