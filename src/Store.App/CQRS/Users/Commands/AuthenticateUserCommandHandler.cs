using FluentValidation;
using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.User.Commands;
using Store.Core.Contracts.Users;
using Store.Core.Utils.Validators.User;

namespace Store.App.CQRS.Users.Commands.Update;

public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, string>
{
    private readonly IUserManager _manager;

    public AuthenticateUserCommandHandler(IUserManager manager) =>
        _manager = manager;

    public async Task<string> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        new EmailValidator().Validate(command.AuthData.Email, options =>
        {
            options.ThrowOnFailures();
        });
        new PasswordValidator().Validate(command.AuthData.Password, options =>
        {
            options.ThrowOnFailures();
        });

        return await _manager.AuthenticateAsync(command.AuthData.Email, command.AuthData.Password);
    }
}