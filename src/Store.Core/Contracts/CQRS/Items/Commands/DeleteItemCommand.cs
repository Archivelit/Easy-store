namespace Store.Core.Contracts.CQRS.Items.Commands;

public record DeleteItemCommand(Guid Id) : ICommand;