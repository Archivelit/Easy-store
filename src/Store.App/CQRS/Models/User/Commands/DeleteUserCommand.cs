namespace Store.App.CQRS.Models.User.Commands;

public record DeleteUserCommand(Guid UserId) : ICommand;