using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.Customers.Commands;
using Store.Core.Contracts.Customers;
using Store.Core.Models.Dto.Customers;

namespace Store.App.Customers.Commands;

public class RegisterCustomerCommandHandler : ICommandHandler<RegisterCustomerCommand, CustomerDto>
{
    private readonly ICustomerManager _customerManager;

    public RegisterCustomerCommandHandler(ICustomerManager customerManager) =>
        _customerManager = customerManager;

    public async Task<CustomerDto> Handle(RegisterCustomerCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var customer = await _customerManager.RegisterAsync(command.Name, command.AuthData.Email, command.AuthData.Password);

        return new(customer);
    }
}