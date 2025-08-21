using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.User.Commands;
using Store.Core.Contracts.Repositories;

namespace Store.App.CQRS.Users.Commands.Update;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository) =>
        _userRepository = userRepository;

    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _userRepository.DeleteAsync(command.UserId);
    }
}