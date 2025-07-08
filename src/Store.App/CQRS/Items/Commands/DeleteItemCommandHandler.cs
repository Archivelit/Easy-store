using Store.App.GraphQl.CQRS;
using Store.Core.Contracts.CQRS.Items.Commands;
using Store.Core.Contracts.Repositories;

namespace Store.App.CQRS.Items.Commands;

public class DeleteItemCommandHandler : ICommandHandler<DeleteItemCommand>
{
    private readonly IItemRepository _repository;

    public DeleteItemCommandHandler(IItemRepository repository) =>
        _repository = repository;

    // TODO: add validation
    public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        await _repository.DeleteByIdAsync(request.Id);
    }
}