namespace Store.Core.Contracts.CQRS.User.Commands;

public record RegisterUserQuery(string Email, string Name) : ICommand<UserDto>;