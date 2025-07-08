using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.Customers;

namespace Store.App.CQRS.Customers.Commands.Update.UpdateChain;

public class UpdateCustomerEmail : UpdateCustomerBase
{
    public UpdateCustomerEmail(ICustomerValidator validator) : base(validator) { }

    public override CustomerBuilder Update(CustomerBuilder builder, CustomerDto model)
    {
        if (model.Email != null)
        {
            _validator.ValidateEmail(model.Email);
            builder.WithEmail(model.Email);
        }
        return base.Update(builder, model);
    }
}
