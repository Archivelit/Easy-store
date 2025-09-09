namespace Store.App.CQRS.Models.Item.Commands;

public record CreateItemCommand(CreateItemDto Item) : ICommand<ItemDto>;