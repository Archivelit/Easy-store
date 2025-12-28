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

    /// <summary>
    /// Return item with specified id
    /// </summary>
    /// <returns>
    /// Registered item model
    /// </returns>>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetItemByIdAsync(
        [FromRoute] Guid id, 
        CancellationToken ct)
    {
        var query = new GetItemByIdQuery(id);

        var result = await _mediator.Send(query, ct);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Delete item with specified id
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize("ItemOwner")]
    public async Task<IActionResult> DeleteItemAsync(
        [FromRoute] Guid id, 
        CancellationToken ct)
    {
        var command = new DeleteItemCommand(id);

        await _mediator.Send(command, ct);

        return Ok();
    }

    /// <summary>
    /// Update item with specified id.
    /// </summary>s
    /// <returns>
    /// Updated item model
    /// </returns>
    [HttpPatch("{id:guid}")]
    [Authorize("ItemOwner")]
    public async Task<IActionResult> UpdateItemAsync(
        [FromBody] UpdateItemDto item,
        CancellationToken ct)
    {
        var command = new UpdateItemCommand(item);

        var result = await _mediator.Send(command, ct);

        if (result is null)
            return NotFound(); 
        
        return Ok(result);
    }
}