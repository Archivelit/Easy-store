using Store.Core.Models;

namespace Store.Core.Contracts.Factories;

public interface IItemFactory
{
    Item Create(IItem item);
}