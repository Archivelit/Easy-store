namespace Store.App.GraphQl.CQRS.Customers.Commands;

public record DeleteCustomerCommand(Guid CustomerId) : ICommand;