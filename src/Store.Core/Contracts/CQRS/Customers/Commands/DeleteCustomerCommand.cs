namespace Store.Core.Contracts.CQRS.Customers.Commands;

public record DeleteCustomerCommand(Guid CustomerId) : ICommand;