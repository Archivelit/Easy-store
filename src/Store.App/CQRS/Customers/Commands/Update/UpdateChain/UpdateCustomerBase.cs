using Microsoft.Extensions.Logging;
using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.Customers;

namespace Store.App.CQRS.Customers.Commands.Update.UpdateChain;

public class UpdateCustomerBase : ICustomerUpdateChain
{
    protected ICustomerUpdateChain? _next;
    protected ICustomerValidator _validator;
    protected ILogger _logger;

    public UpdateCustomerBase(ICustomerValidator validator, ILogger logger)
    {
        _validator = validator;
        _logger = logger;
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