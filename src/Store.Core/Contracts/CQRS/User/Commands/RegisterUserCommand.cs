using Store.Core.Models.Dto.User;

namespace Store.Core.Contracts.CQRS.User.Commands;

public record RegisterUserCommand(UserAuthDataDto AuthData, string Name) : ICommand<UserDto>;