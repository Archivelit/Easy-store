using Store.Core.Models.Dto.User;

namespace Store.App.GraphQl.CQRS.User.Commands;

public record AuthenticateUserCommand(UserAuthDataDto AuthData) : ICommand<string>;