namespace Store.App.GraphQl.CQRS.User.Commands;

public record DeleteUserCommand(Guid UserId) : ICommand;