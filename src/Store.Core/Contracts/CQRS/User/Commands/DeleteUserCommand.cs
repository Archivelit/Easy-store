namespace Store.Core.Contracts.CQRS.User.Commands;

public record DeleteUserCommand(Guid UserId) : ICommand;