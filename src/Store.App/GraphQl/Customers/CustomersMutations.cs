using Store.App.GraphQl;
using Store.App.GraphQl.CQRS;
using Store.App.GraphQl.CQRS.Customers.Commands;
using Store.Core.Models.Dto.Customers;

namespace Store.App.GraphQl.Customers;

[ExtendObjectType("Mutation")]
public class CustomersMutations : IGraphQlExtender  
{
    public async Task<CustomerDto> RegisterCustomer(
        [GraphQLName("input")] RegisterCustomerCommand command, 
        [Service] ICommandHandler<RegisterCustomerCommand, CustomerDto> handler, 
        CancellationToken ct)
    {
        return await handler.Handle(command, ct);
    }

    public async Task<string> AuthenticateCustomer(
        [GraphQLName("input")] AuthenticateCustomerCommand command, 
        [Service] ICommandHandler<AuthenticateCustomerCommand, string> handler, 
        CancellationToken ct)
    {
        return await handler.Handle(command, ct);
    }

    public async Task DeleteCustomer(
        [GraphQLName("input")] DeleteCustomerCommand command,
        [Service] ICommandHandler<DeleteCustomerCommand> handler,
        CancellationToken ct)
    {
        await handler.Handle(command, ct);
    }

    public async Task UpdateCustomer(
        [GraphQLName("input")] UpdateCustomerCommand command,
        [Service] ICommandHandler<UpdateCustomerCommand> handler,
        CancellationToken ct)
    {
        await handler.Handle(command, ct);
    }
}