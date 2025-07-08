using Store.App.GraphQl.CQRS;
using Store.Core.Contracts.CQRS.Items.Queries;
using Store.Core.Contracts.Repositories;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Queries;

public class GetItemByIdQueryHandler : IQueryHandler<GetItemByIdQuery, ItemDto>
{
    private readonly IItemRepository _repository;

    public GetItemByIdQueryHandler(IItemRepository repository) =>
        _repository = repository;

    //TODO: add validation
    public async Task<ItemDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        return new(await _repository.GetByIdAsync(request.Id));
    }
}