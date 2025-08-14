using Microsoft.Extensions.Logging;
using Store.App.GraphQl.CQRS;
using Store.Core.Contracts.CQRS.Items.Commands;
using Store.Core.Contracts.Repositories;

namespace Store.App.CQRS.Items.Commands;

public class DeleteItemCommandHandler : ICommandHandler<DeleteItemCommand>
{
    private readonly IItemRepository _repository;
    private readonly ILogger<DeleteItemCommandHandler> _logger;
    public DeleteItemCommandHandler(IItemRepository repository, ILogger<DeleteItemCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    // TODO: add validation
    public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _repository.DeleteByIdAsync(request.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during deleting item {ItemId}", request.Id);
            throw;
        }
    }
}