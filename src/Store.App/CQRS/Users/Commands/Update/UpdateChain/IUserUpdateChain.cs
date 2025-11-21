namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public interface IUserUpdateChain
{
    /// <summary>
    /// Sets next element in the chain. <br/>
    /// Used for setting order of the elements in the <see cref="UserUpdateChainFactory.Create"/> method.
    /// </summary>
    IUserUpdateChain SetNext(IUserUpdateChain next);
    Task<UserBuilder> Update(UserBuilder builder, UpdateUserDto model);
}