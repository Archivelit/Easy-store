using Store.App.GraphQl.CQRS;
using Store.App.GraphQl.CQRS.User.Commands;
using Store.Core.Contracts.Repositories;
using Store.Core.Exceptions.InvalidData;

namespace Store.App.CQRS.Users.Commands.Update;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository) =>
        _userRepository = userRepository;

    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        if(command.UserId.Equals(Guid.Empty)) throw new InvalidUserDataException("UserId cannot be empty");
        
        await _userRepository.DeleteAsync(command.UserId);
    }
}