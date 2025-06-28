using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.Customers.Commands;
using Store.Core.Contracts.Repositories;
using Store.Core.Exceptions.InvalidData.Item;

namespace Store.App.Customers.Commands;

public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository) =>
        _customerRepository = customerRepository;

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        if(request.CustomerId == Guid.Empty) throw new InvalidCustomerId("CustomerId cannot be empty");
        
        await _customerRepository.DeleteAsync(request.CustomerId);
    }
}