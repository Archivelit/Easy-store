using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.Customers.Commands;
using Store.Core.Contracts.Customers;

namespace Store.App.Customers.Commands;

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