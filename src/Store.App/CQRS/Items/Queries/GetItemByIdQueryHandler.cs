using Microsoft.Extensions.Logging;
using Store.App.GraphQl.CQRS;
using Store.Core.Contracts.CQRS.Items.Queries;
using Store.Core.Contracts.Repositories;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Queries;

public class GetItemByIdQueryHandler : IQueryHandler<GetItemByIdQuery, ItemDto>
{
    private readonly IItemRepository _repository;
    private readonly ILogger<GetItemByIdQueryHandler> _logger;

    public GetItemByIdQueryHandler(IItemRepository repository, ILogger<GetItemByIdQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    //TODO: add validation
    public async Task<ItemDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        {
            _logger.LogDebug("Item {ItemId} requested", request.Id);
            
            cancellationToken.ThrowIfCancellationRequested();

            return new(await _repository.GetByIdAsync(request.Id));
        }
    }
}