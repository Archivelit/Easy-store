using Store.App.GraphQl.CQRS;
using Store.App.GraphQl.Items;
using Store.Core.Contracts.Repositories;
using Store.App.GraphQl.Validation;
using Store.Core.Contracts.CQRS.Items.Commands;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands;

public class CreateItemCommandHandler : ICommandHandler<CreateItemCommand, ItemDto>
{
    private readonly IItemRepository _repository;
    private readonly IItemValidator _itemValidator;
    private readonly IItemFactory _itemFactory;
    
    public CreateItemCommandHandler(IItemRepository repository, IItemValidator itemValidator, IItemFactory itemFactory)
    {
        _repository = repository;
        _itemValidator = itemValidator;
        _itemFactory = itemFactory;
    }
    
    public async Task<ItemDto> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        command = command with
        {
            Item = command.Item with
            {
                Title = command.Item.Title.Trim(), 
                Description = command.Item.Description?.Trim()
            }
        };
        
        _itemValidator.ValidateAndThrow(command.Item);
        
        var itemEntity = _itemFactory.Create(command.Item);
        
        await _repository.RegisterAsync(itemEntity);

        return new(itemEntity);
    }
}