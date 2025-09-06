namespace Store.Core.Contracts.CQRS.Item.Commands;

public record CreateItemCommand(CreateItemDto Item) : ICommand<ItemDto>;