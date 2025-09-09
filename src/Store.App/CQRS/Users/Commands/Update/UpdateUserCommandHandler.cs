namespace Store.App.CQRS.Users.Commands.Update;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, UserDto>
{
    
    private readonly UserUpdateFacade _facade;
    
    public UpdateUserCommandHandler(UserUpdateFacade facade)
    {
        _facade = facade;
    }

    public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        return await _facade.UpdateUserAsync(command.UserDto);
    }
}