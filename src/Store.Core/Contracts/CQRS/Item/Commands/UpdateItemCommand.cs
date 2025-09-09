namespace Store.Core.Contracts.CQRS.Item.Commands;

public record UpdateItemCommand(UpdateItemDto Item) : ICommand<ItemDto>;