namespace Store.App.CQRS.Users.Commands.Update.UpdateChain;

public interface IUserUpdateChainFactory
{
    IUserUpdateChain Create();
}