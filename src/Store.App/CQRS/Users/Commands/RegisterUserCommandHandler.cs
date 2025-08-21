using FluentValidation;
using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.User.Commands;
using Store.Core.Contracts.Users;
using Store.Core.Models.Dto.User;
using Store.Core.Utils.Validators.User;

namespace Store.App.CQRS.Users.Commands.Update;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserDto>
{
    private readonly IUserManager _userManager;

    public RegisterUserCommandHandler(IUserManager userManager) =>
        _userManager = userManager;

    public async Task<UserDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
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

        var user = await _userManager.RegisterAsync(command.Name, command.AuthData.Email, command.AuthData.Password);

        return new(user);
    }
}