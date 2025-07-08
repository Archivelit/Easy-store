using Store.App.GraphQl.CQRS;
using Store.App.GraphQl.CQRS.Customers.Commands;
using Store.App.GraphQl.Customers;

namespace Store.App.CQRS.Customers.Commands.Update;

public class AuthenticateCustomerCommandHandler : ICommandHandler<AuthenticateCustomerCommand, string>
{
    private readonly ICustomerManager _manager;

    public AuthenticateCustomerCommandHandler(ICustomerManager manager) =>
        _manager = manager;

    public async Task<string> Handle(AuthenticateCustomerCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _manager.AuthenticateAsync(command.AuthData.Email, command.AuthData.Password);
    }
}