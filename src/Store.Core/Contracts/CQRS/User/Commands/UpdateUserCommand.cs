using Store.Core.Models.Dto.User;

namespace Store.App.GraphQl.CQRS.User.Commands;

public record UpdateUserCommand(UserDto UserDto, string Password) : ICommand;