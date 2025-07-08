using Store.App.GraphQl.Validation;
using Store.Core.Builders;
using Store.Core.Models.Dto.Customers;

namespace Store.App.CQRS.Customers.Commands.Update.UpdateChain;

public class UpdateCustomerSubscription : UpdateCustomerBase
{
    public UpdateCustomerSubscription(ICustomerValidator validator) : base(validator) { }

    public override CustomerBuilder Update(CustomerBuilder builder, CustomerDto model)
    {
        if (model.SubscriptionType != null)
        {
            _validator.ValidateSubscription(model.SubscriptionType.ToString());
            builder.WithSubscriptionType(model.SubscriptionType);
        }
        return base.Update(builder, model);
    }
}