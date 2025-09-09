namespace Store.App.CQRS.Models.Item.Commands;

public record UpdateItemCommand(UpdateItemDto Item) : ICommand<ItemDto>;