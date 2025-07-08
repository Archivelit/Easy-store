using Store.App.GraphQl.CQRS;
using Store.App.GraphQl.CQRS.Customers.Commands;
using Store.Core.Contracts.Repositories;
using Store.Core.Exceptions.InvalidData.Item;

namespace Store.App.CQRS.Customers.Commands.Update;

public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository) =>
        _customerRepository = customerRepository;

    public async Task Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        if(command.CustomerId == Guid.Empty) throw new InvalidCustomerId("CustomerId cannot be empty");
        
        await _customerRepository.DeleteAsync(command.CustomerId);
    }
}