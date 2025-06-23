using Microsoft.EntityFrameworkCore;
using Store.Infrastructure.Data.Postgres;
using Store.Core.Contracts.Items;
using Store.Core.Models.DTO.Items;
using Store.Core.Exceptions.InvalidData.Item;

namespace Store.API.Utils.Validators;

public class ItemDtoValidator : IItemDtoValidator
{
    private readonly AppDbContext _context;
    
    public ItemDtoValidator(AppDbContext context)
    {
        _context = context;
    }
    
    public void ValidateAndThrow(ItemDto dto)
    {
        ValidateTitle(dto.Title);
        ValidateDescription(dto.Description);
        ValidatePrice(dto.Price);
        ValidateQuantity(dto.QuantityInStock);
        ValidateCustomerId(dto.CustomerId);
        ValidateCreationDate(dto.CreatedAt);
        ValidateUpdateDate(dto.UpdatedAt, dto.CreatedAt);
    }

    private void ValidateTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new InvalidItemTitle("Item title cannot be null or empty.");

        if (title.Length < 3)
            throw new InvalidItemTitle("Title is too short. It has to have at least 3 characters.");

        if (title.Length > 100)
            throw new InvalidItemTitle("Title is too long. It has to be 100 or less characters.");

        if (!title.Any(char.IsLetter))
            throw new InvalidItemTitle("Invalid item title. It must contain at least 1 letter.");
    }

    private void ValidateDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return;
        
        if (description.Length > 1000)
            throw new InvalidItemDescription("Description is too long. It has to be 1000 or less characters.");
        
        if (!description.Any(char.IsLetter))
            throw new InvalidItemDescription("Description must contain letters.");
    }

    private async void ValidateCustomerId(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new InvalidCustomerId("Customer ID cannot be empty.");
        
        if (! await _context.Customers.AnyAsync(x => x.Id == customerId))
            throw new InvalidCustomerId("Customer with given ID was not found.");
    }

    private void ValidatePrice(decimal price)
    {
        if (price < 0)
            throw new InvalidItemPrice("Price must be greater than zero.");
    }

    private void ValidateQuantity(int quantity)
    {
        if (quantity < 0)
            throw new InvalidItemQuantity("Quantity must be greater than zero.");
    }

    private void ValidateCreationDate(DateTime creationDate)
    {
        if (creationDate > DateTime.UtcNow)
            throw new InvalidItemCreateTime("Creation date cannot be in the future.");
    }

    private void ValidateUpdateDate(DateTime? updateDate, DateTime creationDate)
    {
        if (updateDate == null)
            return;

        if (updateDate.Value.Date > DateTime.UtcNow)
            throw new InvalidItemUpdateTime("Update date cannot be in the future.");

        if (updateDate.Value.Date < creationDate)
            throw new InvalidItemUpdateTime("Update date cannot be earlier than creation date.");
    }
}