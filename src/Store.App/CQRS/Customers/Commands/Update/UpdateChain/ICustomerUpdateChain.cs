using Store.Core.Builders;
using Store.Core.Models.Dto.Customers;

namespace Store.App.CQRS.Customers.Commands.Update.UpdateChain;

public interface ICustomerUpdateChain
{
    ICustomerUpdateChain SetNext(ICustomerUpdateChain next);
    CustomerBuilder Update(CustomerBuilder builder, CustomerDto model);
}