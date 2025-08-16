using Store.Core.Models.Dto.User;

namespace Store.App.GraphQl.CQRS.User.Commands;

public record RegisterUserCommand(UserAuthDataDto AuthData, string Name) : ICommand<UserDto>;