using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Factories;
using Store.Core.Contracts.Items;
using Store.Core.Models.DTO.Items;
using Store.Infrastructure.Data.Postgres;

namespace Store.API.Controllers.Items;

[Route("api/items")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IItemDtoValidator _itemDtoValidator;
    private readonly IItemFactory _itemFactory;

    public ItemController(AppDbContext context, IItemDtoValidator itemDtoValidator, IItemFactory itemFactory)
    {
        _context = context;
        _itemDtoValidator = itemDtoValidator;
        _itemFactory = itemFactory;
    }

    // PUT: api/items/register/
    // Register item in db
    //
    // Params:
    //  CustomerId - customer id
    //  Title - item title
    //  Price - item price
    //  QuantityInStock - quantity of items in stock
    //  CreatedAt - creation date
    //  <OPTIONAL>
    //  UpdatedAt - update date
    //  Description - item description
    // 
    [HttpPut ("register")]
    public async Task<IActionResult> PutItemEntity(ItemDto itemDto)
    {
        itemDto = itemDto with { Title = itemDto.Title.Trim(),  Description = itemDto.Description?.Trim() };
        
        _itemDtoValidator.ValidateAndThrow(itemDto);
        
        var itemEntity = _itemFactory.Create(itemDto);
        
        _context.Entry(itemEntity).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteItemEntity(Guid id)
    {
        _context.Remove(await _context.Items.FirstOrDefaultAsync(i => i.Id == id));
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}