using Store.Core.Enums;

namespace Store.App.CQRS.Users.Commands.Update;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserDto>
{
    private readonly ILogger<RegisterUserCommandHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<string> _passwordValidator;
    private readonly IValidator<string> _emailValidator;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IUserRepository userRepository, ILogger<RegisterUserCommandHandler> logger, 
        [FromKeyedServices("PasswordValidator")] IValidator<string> passwordValidator, 
        [FromKeyedServices("EmailValidator")]IValidator<string> emailValidator, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _logger = logger;
        _passwordValidator = passwordValidator;
        _emailValidator = emailValidator;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _emailValidator.ValidateAndThrow(command.user.Email);

        _passwordValidator.ValidateAndThrow(command.user.Password);

        _logger.LogDebug("Registering users");

        try
        {
            if (await _userRepository.IsEmailClaimedAsync(command.user.Email))
            {
                throw new InvalidUserDataException("Email is already registered");
            }

            var hashedPassword = _passwordHasher.Hash(command.user.Password);
            var userId = Guid.NewGuid();

            var credentials = new UserCredentials(userId, Role.User, hashedPassword);
            var user = new User(userId, command.user.Name, command.user.Email, Subscription.None);
            
            await _userRepository.RegisterAsync(user, credentials);

            _logger.LogDebug("User registered");

            return new(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during registration");
            throw;
        }
    }
}