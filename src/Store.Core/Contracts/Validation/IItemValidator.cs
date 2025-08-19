using Store.Core.Models;

namespace Store.Core.Contracts.Validation;

public interface IItemValidator
{
    void ValidateAndThrow(IItem itemDto);
    void ValidateTitle(string title);
    void ValidateDescription(string? description);
    void ValidateCustomerId(Guid customerId);
    void ValidatePrice(decimal price);
    void ValidateQuantity(int quantity);
    void ValidateUpdateDate(DateTime? updateDate, DateTime creationDate);
}