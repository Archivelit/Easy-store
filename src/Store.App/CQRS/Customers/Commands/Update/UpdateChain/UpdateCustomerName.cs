using Microsoft.Extensions.Logging;
using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.Customers;

namespace Store.App.CQRS.Customers.Commands.Update.UpdateChain;

public class UpdateCustomerName : UpdateCustomerBase
{
    public UpdateCustomerName(ICustomerValidator validator, ILogger logger) : base(validator, logger) { }

    public override CustomerBuilder Update(CustomerBuilder builder, CustomerDto model)
    {
        if (model.Name != null)
        {
            _validator.ValidateCustomerName(model.Name);
            builder.WithName(model.Name);
            _logger.LogDebug("User {UserId} updated name to {NewUserName}", model.Id, model.Name);
        }
        return base.Update(builder, model);
    }
}