using Store.Core.Contracts.Models;

namespace Store.Core.Contracts.Validation;

public interface IItemValidator
{
    void ValidateAndThrow(IItem itemDto);
}