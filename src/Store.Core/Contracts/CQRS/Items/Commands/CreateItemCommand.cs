using Store.App.GraphQl.CQRS;
using Store.Core.Models.Dto.Items;

namespace Store.Core.Contracts.CQRS.Items.Commands;

public record CreateItemCommand(CreateItemDto Item) : ICommand<ItemDto>;