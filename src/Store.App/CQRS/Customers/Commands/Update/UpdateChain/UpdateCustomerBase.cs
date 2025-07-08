using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.Customers;

namespace Store.App.CQRS.Customers.Commands.Update.UpdateChain;

public class UpdateCustomerBase : ICustomerUpdateChain
{
    protected ICustomerUpdateChain? _next;
    protected ICustomerValidator _validator;

    public UpdateCustomerBase(ICustomerValidator validator)
    {
        _validator = validator;
    }

    public ICustomerUpdateChain SetNext(ICustomerUpdateChain next)
    {
        _next = next;
        return next;
    }

    public virtual CustomerBuilder Update(CustomerBuilder builder,CustomerDto model)
    {
        return _next.Update(builder, model);
    }
}