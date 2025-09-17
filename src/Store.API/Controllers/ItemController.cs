namespace Store.API.Controllers;

[ApiController]
[Route("items")]
public class ItemController : ControllerBase
{
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetItemByIdAsync(
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

    [HttpDelete("{id:guid}")]
    [Authorize("ItemOwner")]
    public async Task<IActionResult> DeleteItemAsync(
        Guid id, 
        CancellationToken ct,
        [FromServices] ICommandHandler<DeleteItemCommand> handler)
    {
        await handler.Handle(new DeleteItemCommand(id), ct);

        return Ok();
    }

    [HttpPost]
    [Authorize("UserOrAdministrator")]
    public async Task<IActionResult> CreateItemAsync(
        [FromBody] CreateItemDto item,
        CancellationToken ct,
        [FromServices] ICommandHandler<CreateItemCommand, ItemDto> handler)
    {
        var result = await handler.Handle(new CreateItemCommand(item), ct);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(item);
    }

    [HttpPatch("{id:guid}")]
    [Authorize("ItemOwner")]
    public async Task<IActionResult> UpdateItemAsync(
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