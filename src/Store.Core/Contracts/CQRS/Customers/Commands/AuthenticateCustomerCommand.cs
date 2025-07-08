using Store.Core.Models.Dto.Customers;

namespace Store.App.GraphQl.CQRS.Customers.Commands;

public record AuthenticateCustomerCommand(CustomerAuthDataDto AuthData) : ICommand<string>;