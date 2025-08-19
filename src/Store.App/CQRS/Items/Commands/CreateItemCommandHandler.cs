using Store.Core.Contracts.Repositories;
using Store.Core.Contracts.CQRS.Item.Commands;
using Store.Core.Models.Dto.Items;
using Microsoft.Extensions.Logging;
using Store.Core.Contracts.Factories;
using Store.Core.Contracts.Validation;
using Store.Core.Contracts.CQRS;

namespace Store.App.CQRS.Items.Commands;

public class CreateItemCommandHandler : ICommandHandler<CreateItemCommand, ItemDto>
{
    private readonly IItemRepository _repository;
    private readonly IItemValidator _itemValidator;
    private readonly IItemFactory _itemFactory;
    private readonly ILogger<CreateItemCommandHandler> _logger;

    public CreateItemCommandHandler(IItemRepository repository, IItemValidator itemValidator, IItemFactory itemFactory, ILogger<CreateItemCommandHandler> logger)
    {
        _repository = repository;
        _itemValidator = itemValidator;
        _itemFactory = itemFactory;
        _logger = logger;
    }

    public async Task<ItemDto> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Starting item registration");

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

            _logger.LogInformation("Item {ItemId} registered", itemEntity.Id);
            
            return new(itemEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during registring item");
            throw;
        }
    }
}