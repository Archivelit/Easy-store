using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.Items.Commands;
using Store.Core.Contracts.Items;
using Store.Core.Contracts.Repositories;
using Store.Core.Contracts.Validation;
using Store.Core.Models.Dto.Items;

namespace Store.App.Items.Commands;

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
        
        await _repository.RegisterItemAsync(itemEntity);

        return new(itemEntity);
    }
}