using Store.Core.Models.Dto.User;

namespace Store.Core.Contracts.CQRS.User.Commands;

public record AuthenticateUserCommand(UserAuthDataDto AuthData) : ICommand<string>;