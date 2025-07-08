using Store.App.GraphQl.Models;

namespace Store.App.GraphQl.Validation;

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