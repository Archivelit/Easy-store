using Microsoft.AspNetCore.Mvc;
using MediatR;
using Store.Core.Contracts.CQRS.Items.Commands;
using Store.Core.Contracts.CQRS.Items.Queries;
using Store.Core.Models.Dto.Items;

namespace Store.API.Controllers.Items;

[Route("api/items")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ItemController(IMediator mediator) => _mediator = mediator;

    // POST: api/items/
    // Register item in db
    //
    // Params:
    //  CustomerId - customer id
    //  Title - item title
    //  Price - item price
    //  QuantityInStock - quantity of items in stock
    //  CreatedAt - creation date
    //  OPTIONAL
    //  UpdatedAt - update date
    //  Description - item description
    // 
    [HttpPut]
    public async Task<ActionResult<ItemDto>> RegisterItem(CreateItemCommand command)
    {
        var createdItem = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetItemById), new { id = createdItem.Id }, createdItem);
    }
    
    // DELETE: api/items/{item id}
    // Delete item in db with specified id
    //
    // Params:
    //  Id - item id
    //
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteItemEntity(Guid id)
    {
        
        var command = new DeleteItemCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    // GET: api/items/{item id}
    // Returns item from db with specified id
    //
    // Params:
    //  Id - item id
    //
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemDto>> GetItemById(Guid id)
    {
        var item = await _mediator.Send(new GetItemByIdQuery(id));
        return Ok(item);
    }
    
    // TODO: write endpoint for getting some random items
}