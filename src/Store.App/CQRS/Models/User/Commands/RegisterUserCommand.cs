using Store.Core.Models.Dto.Auth;

namespace Store.App.CQRS.Models.User.Commands;

public record RegisterUserCommand(RegisterUserDto user) : ICommand<UserDto>;