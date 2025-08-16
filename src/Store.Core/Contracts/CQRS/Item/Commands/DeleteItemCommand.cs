using Store.App.GraphQl.CQRS;

namespace Store.Core.Contracts.CQRS.Item.Commands;

public record DeleteItemCommand(Guid Id) : ICommand;