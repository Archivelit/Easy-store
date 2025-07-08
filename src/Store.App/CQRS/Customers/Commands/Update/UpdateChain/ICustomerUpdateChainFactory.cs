namespace Store.App.CQRS.Customers.Commands.Update.UpdateChain;

public interface ICustomerUpdateChainFactory
{
    ICustomerUpdateChain Create();
}