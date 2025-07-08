using Store.Core.Models.Dto.Customers;

namespace Store.App.GraphQl.CQRS.Customers.Commands;

public record UpdateCustomerCommand(CustomerDto CustomerDto, string Password) : ICommand;