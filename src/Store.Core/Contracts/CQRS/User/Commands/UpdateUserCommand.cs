namespace Store.Core.Contracts.CQRS.User.Commands;

// TODO force it return updated user
public record UpdateUserCommand(UserDto UserDto) : ICommand;