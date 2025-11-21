namespace Store.App.CQRS.Items.Queries;

public sealed class GetItemByIdQueryHandler : IQueryHandler<GetItemByIdQuery, ItemDto>
{
    private readonly IItemRepository _repository;
    private readonly ILogger<GetItemByIdQueryHandler> _logger;

    public GetItemByIdQueryHandler(IItemRepository repository, ILogger<GetItemByIdQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ItemDto> Handle(GetItemByIdQuery request, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        _logger.LogDebug("Item {ItemId} requested", request.Id);

        return new (await _repository.GetByIdAsync(request.Id));
    }
}