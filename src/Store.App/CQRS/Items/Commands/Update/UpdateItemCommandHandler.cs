using Store.Core.Contracts.CQRS;
using Store.Core.Contracts.CQRS.Item.Commands;
using Store.Core.Models.Dto.Items;

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
        
        return await _facade.UpdateItemAsync(command.ItemDto);
    }
}
