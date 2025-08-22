using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.User.Commands;

namespace Store.App.CQRS.Users.Commands.Update;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    
    private readonly UserUpdateFacade _facade;
    
    public UpdateUserCommandHandler(UserUpdateFacade facade)
    {
        _facade = facade;
    }

    public async Task Handle(UpdateUserCommand command, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        await _facade.UpdateUserAsync(command.UserDto);
    }
}