using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.Customers;

namespace Store.App.CQRS.Customers.Commands.Update.UpdateChain;

public class UpdateCustomerName : UpdateCustomerBase
{
    public UpdateCustomerName(ICustomerValidator validator) : base(validator) { }

    public override CustomerBuilder Update(CustomerBuilder builder, CustomerDto model)
    {
        if (model.Name != null)
        {
            _validator.ValidateCustomerName(model.Name);
            builder.WithName(model.Name);
        }
        return base.Update(builder, model);
    }
}