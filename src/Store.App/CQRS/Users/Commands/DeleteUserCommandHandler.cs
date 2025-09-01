using Microsoft.Extensions.Logging;
using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.User.Commands;
using Store.Core.Contracts.Repositories;

namespace Store.App.CQRS.Users.Commands.Update;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly ILogger<DeleteUserCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            _logger.LogInformation("Deleting user {UserId}", command.UserId);
            await _userRepository.DeleteAsync(command.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during deleting user {UserId}", command.UserId);
            throw;
        }
    }
}