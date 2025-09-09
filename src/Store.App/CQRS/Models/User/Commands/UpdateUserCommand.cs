namespace Store.App.CQRS.Models.User.Commands;

public record UpdateUserCommand(UserDto UserDto) : ICommand<UserDto>;