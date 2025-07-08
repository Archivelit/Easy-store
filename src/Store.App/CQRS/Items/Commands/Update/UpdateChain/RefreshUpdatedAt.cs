using Store.Core.Builders;
using Store.Core.Models.Dto.Items;

namespace Store.App.CQRS.Items.Commands.Update.UpdateChain;

public class RefreshUpdatedAt : ItemUpdateChainBase
{
    public override ItemBuilder Update(ItemBuilder builder, UpdateItemDto itemDto)
    {
        builder.WithUpdatedAt(DateTime.UtcNow);
        return base.Update(builder, itemDto);
    }
}