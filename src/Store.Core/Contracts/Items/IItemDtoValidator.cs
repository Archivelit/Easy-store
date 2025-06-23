using Store.Core.Models.DTO.Items;

namespace Store.Core.Contracts.Items;

public interface IItemDtoValidator
{
    void ValidateAndThrow(ItemDto itemDto);
}