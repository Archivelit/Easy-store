using Store.App.GraphQl.CQRS;
using Store.App.GraphQl.CQRS.Customers.Commands;
using Store.App.GraphQl.Customers;
using Store.Core.Models.Dto.Customers;

namespace Store.App.CQRS.Customers.Commands.Update;

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