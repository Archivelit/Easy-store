using Store.App.CQRS.Items.Commands.Update.UpdateChain;
using Store.Core.Builders;
using Store.Core.Contracts.Repositories;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update;

public class ItemUpdateFacade
{
    private readonly IItemUpdateChain _chain;
    private readonly IItemRepository _itemRepository;

    public ItemUpdateFacade(IItemUpdateChainFactory factory, IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
        _chain = factory.Create();
    }

    public async Task<ItemDto> UpdateCustomerAsync(UpdateItemDto itemDto)
    {
        var itemData = await _itemRepository.GetByIdAsync(itemDto.Id);

        var builder = new ItemBuilder();
        builder.From(itemData);

        var updateData = GetNewData(builder, itemDto);

        await _itemRepository.UpdateAsync(updateData);

        return updateData;
    }

    private ItemDto GetNewData(ItemBuilder builder, UpdateItemDto itemDto)
    {
        builder = _chain.Update(builder, itemDto);
        
        var item = builder.Build();
        
        return new ItemDto(item);
    }
}