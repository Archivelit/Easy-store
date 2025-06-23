using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Core.Factories;
using Store.Core.Contracts.Items;
using Store.Core.Models.DTO.Items;
using Store.Infrastructure.Data.Postgres;
using Store.Infrastructure.Mappers;

namespace Store.API.Controllers.Items;

[Route("api/items")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IItemDtoValidator _itemDtoValidator;
    private readonly ItemFactory _itemFactory;

    public RegistrationController(AppDbContext context, IItemDtoValidator itemDtoValidator, ItemFactory itemFactory)
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
        
        var itemEntity = ItemMapper.ToEntity(_itemFactory.Create(itemDto));
        
        _context.Entry(itemEntity).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
}