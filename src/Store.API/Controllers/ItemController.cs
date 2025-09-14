namespace Store.API.Controllers;

using Store.App.CQRS.Models.Item.Commands;
using Store.App.CQRS.Models.Item.Queries;
using Store.Core.Models.Dto.Item;

[ApiController]
[Route("items")]
public class ItemController : ControllerBase
{
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<ItemDto>> GetItemByIdAsync(
        Guid id, 
        CancellationToken ct,
        [FromServices] IQueryHandler<GetItemByIdQuery, ItemDto> handler)
    {
        var result = await handler.Handle(new GetItemByIdQuery(id), ct);
        
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [Authorize("ItemOwner")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteItemAsync(
        Guid id, 
        CancellationToken ct,
        [FromServices] ICommandHandler<DeleteItemCommand> handler)
    {
        await handler.Handle(new DeleteItemCommand(id), ct);

        return Ok();
    }

    [HttpPost]
    [Authorize("UserOrAdministrator")]
    public async Task<ActionResult> CreateItemAsync(
        [FromBody] CreateItemDto item,
        CancellationToken ct,
        [FromServices] ICommandHandler<CreateItemCommand, ItemDto> handler)
    {
        await handler.Handle(new CreateItemCommand(item), ct);

        return Ok();
    }

    [HttpPost("{id:guid}/update")]
    [Authorize("ItemOwner")]
    public async Task<ActionResult<ItemDto>> UpdateItemAsync(
        [FromBody] UpdateItemDto item,
        CancellationToken ct,
        [FromServices] ICommandHandler<UpdateItemCommand, ItemDto> handler)
    {
        var result = await handler.Handle(new UpdateItemCommand(item), ct);
        
        if (result is null)
        {
            return NotFound(); 
        }
        
        return Ok(result);
    }
}