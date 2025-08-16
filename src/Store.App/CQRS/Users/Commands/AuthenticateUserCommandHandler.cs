using Store.App.GraphQl.CQRS;
using Store.App.GraphQl.CQRS.User.Commands;
using Store.App.GraphQl.Users;

namespace Store.App.CQRS.Users.Commands.Update;

public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, string>
{
    private readonly IUserManager _manager;

    public AuthenticateUserCommandHandler(IUserManager manager) =>
        _manager = manager;

    public async Task<string> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _manager.AuthenticateAsync(command.AuthData.Email, command.AuthData.Password);
    }
}