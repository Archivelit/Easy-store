namespace Store.App.CQRS.Items.Commands.Update;

public class UpdateItemCommandHandler : ICommandHandler<UpdateItemCommand, ItemDto>
{
    
    private readonly ItemUpdateFacade _facade;
    
    public UpdateItemCommandHandler(ItemUpdateFacade facade)
    {
        _facade = facade;
    }

    public async Task<ItemDto> Handle(UpdateItemCommand command, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        
        var result = await _facade.UpdateItemAsync(command.Item);

        return result;
    }
}