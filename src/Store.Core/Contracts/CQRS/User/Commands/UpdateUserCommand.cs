using Store.Core.Models.Dto.User;

namespace Store.Core.Contracts.CQRS.User.Commands;

public record UpdateUserCommand(UserDto UserDto, string Password) : ICommand;