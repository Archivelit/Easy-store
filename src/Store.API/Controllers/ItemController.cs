namespace Store.API.Controllers;

[ApiController]
[Route("items")]
public class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetItemByIdAsync(
        Guid id, 
        CancellationToken ct)
    {
        var query = new GetItemByIdQuery(id);

        var result = await _mediator.Send(query, ct);

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
        var command = new DeleteItemCommand(id);

        await _mediator.Send(command, ct);

        return Ok();
    }

    [HttpPost]
    [Authorize("UserOrAdministrator")]
    public async Task<IActionResult> CreateItemAsync(
        [FromBody] CreateItemDto item,
        CancellationToken ct)
    {
        var command = new CreateItemCommand(item);

        var result = await _mediator.Send(command, ct);

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
        CancellationToken ct)
    {
        var command = new UpdateItemCommand(item);

        var result = await _mediator.Send(command, ct);

        if (result is null)
        {
            return NotFound(); 
        }
        
        return Ok(result);
    }

    [HttpGet("/image/{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetImage(
        Guid id,
        CancellationToken ct)
    {
        var query = new GetLinkToItemProfileImageQuery(id);
        
        var result = await _mediator.Send(query, ct);
        
        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}