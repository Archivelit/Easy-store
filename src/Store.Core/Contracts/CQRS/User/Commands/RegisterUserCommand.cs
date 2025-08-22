using Store.Core.Models.Dto.User;

namespace Store.Core.Contracts.CQRS.User.Commands;

public record RegisterUserCommand(string Email, string Name) : ICommand<UserDto>;