namespace Store.App.CQRS.Models.Item.Commands;

public record DeleteItemCommand(Guid Id) : ICommand;