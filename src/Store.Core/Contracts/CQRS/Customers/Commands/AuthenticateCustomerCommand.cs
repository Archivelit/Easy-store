using Store.Core.Models.Dto.Customers;

namespace Store.Core.Contracts.CQRS.Customers.Commands;

public record AuthenticateCustomerCommand(CustomerAuthDataDto AuthData) : ICommand<string>;