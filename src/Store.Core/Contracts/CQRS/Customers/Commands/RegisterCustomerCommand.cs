using Store.Core.Models.Dto.Customers;

namespace Store.App.GraphQl.CQRS.Customers.Commands;

public record RegisterCustomerCommand(CustomerAuthDataDto AuthData, string Name) : ICommand<CustomerDto>;