namespace Store.App.CQRS.Models.User.Commands;

public record RegisterUserCommand(string Email, string Name) : ICommand<UserDto>;