using Store.Core.Contracts.Repositories;
using Store.Core.Contracts.CQRS.Item.Commands;
using Store.Core.Models.Dto.Items;
using Microsoft.Extensions.Logging;
using Store.Core.Contracts.Factories;
using Store.Core.Contracts.CQRS;
using FluentValidation;
using Store.Core.Models;

namespace Store.App.CQRS.Items.Commands;

public class CreateItemCommandHandler : ICommandHandler<CreateItemCommand, ItemDto>
{
    private readonly IItemRepository _repository;
    private readonly IValidator<Item> _validator;
    private readonly IItemFactory _itemFactory;
    private readonly ILogger<CreateItemCommandHandler> _logger;

    public CreateItemCommandHandler(IItemRepository repository, IItemFactory itemFactory, ILogger<CreateItemCommandHandler> logger, IValidator<Item> validator)
    {
        _repository = repository;
        _itemFactory = itemFactory;
        _logger = logger;
        _validator = validator;
    }

    public async Task<ItemDto> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            _logger.LogDebug("Starting item registration");

            var item = _itemFactory.Create(command.Item);

            _validator.Validate(item, options =>
            {
                options.IncludeRuleSets("*");
                options.ThrowOnFailures();
            });

            await _repository.RegisterAsync(item);

            _logger.LogInformation("Item {ItemId} registered", item.Id);
            
            return new(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during registring item");
            throw;
        }
    }
}