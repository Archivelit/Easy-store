namespace Store.App.CQRS.Items.Commands;

public class DeleteItemCommandHandler : ICommandHandler<DeleteItemCommand>
{
    private readonly IItemRepository _itemRepository;
    private readonly ILogger<DeleteItemCommandHandler> _logger;

    public DeleteItemCommandHandler(IItemRepository itemRepository, ILogger<DeleteItemCommandHandler> logger)
    {
        _itemRepository = itemRepository;
        _logger = logger;
    }

    public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _itemRepository.DeleteByIdAsync(request.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during deleting item {ItemId}", request.Id);
            throw;
        }
    }
}