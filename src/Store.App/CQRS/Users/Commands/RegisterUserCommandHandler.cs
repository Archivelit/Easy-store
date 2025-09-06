namespace Store.App.CQRS.Users.Commands.Update;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserDto>
{
    private readonly ILogger<RegisterUserCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository, ILogger<RegisterUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<UserDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        new EmailValidator().Validate(command.Email, options => 
        {
            options.ThrowOnFailures();
        });

        _logger.LogDebug("Starting user registration");

        try
        {
            if (await _userRepository.IsEmailClaimedAsync(command.Email))
            {
                throw new InvalidUserDataException("Email is already registered");
            }

            var user = new User(command.Name, command.Email);
            await _userRepository.RegisterAsync(user);

            _logger.LogDebug("Ending user registration");

            return new(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during registration");
            throw;
        }
    }
}