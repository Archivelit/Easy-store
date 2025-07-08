using Store.App.GraphQl.CQRS;
using Store.App.GraphQl.CQRS.Customers.Commands;

namespace Store.App.CQRS.Customers.Commands.Update;

public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
{
    
    private readonly CustomerUpdateFacade _facade;
    
    public UpdateCustomerCommandHandler(CustomerUpdateFacade facade)
    {
        _facade = facade;
    }

    public async Task Handle(UpdateCustomerCommand command, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        await _facade.UpdateCustomerAsync(command.CustomerDto, command.Password);
    }
}