using Microsoft.Extensions.Logging;
using Store.App.CQRS.Items.Commands.Update.UpdateChain;
using Store.Core.Builders;
using Store.Core.Contracts.Repositories;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update;

public class ItemUpdateFacade
{
    private readonly IItemUpdateChain _chain;
    private readonly IItemRepository _itemRepository;
    private readonly ILogger<ItemUpdateFacade> _logger;

    public ItemUpdateFacade(IItemUpdateChainFactory factory, IItemRepository itemRepository, ILogger<ItemUpdateFacade> logger)
    {
        _itemRepository = itemRepository;
        _chain = factory.Create();
        _logger = logger;
    }

    public async Task<ItemDto> UpdateItemAsync(UpdateItemDto itemDto)
    {
        try
        {
            _logger.LogInformation("Starting update of {ItemId}", itemDto.Id);

            var itemData = await _itemRepository.GetByIdAsync(itemDto.Id);

            var builder = new ItemBuilder();
            builder.From(itemData);

            var updateData = GetNewData(builder, itemDto);

            await _itemRepository.UpdateAsync(updateData);

            _logger.LogInformation("Item {ItemId} updated succesfuly", itemDto.Id);

            return updateData;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured during Updating Item");
            throw;
        }
    }

    private ItemDto GetNewData(ItemBuilder builder, UpdateItemDto itemDto)
    {
        builder = _chain.Update(builder, itemDto);
        
        var item = builder.Build();
        
        return new ItemDto(item);
    }
}