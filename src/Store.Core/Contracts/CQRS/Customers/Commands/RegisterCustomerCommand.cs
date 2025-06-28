using System.ComponentModel.DataAnnotations;
using Store.Core.Models.Dto.Customers;

namespace Store.Core.Contracts.CQRS.Customers.Commands;

public record RegisterCustomerCommand(CustomerAuthDataDto AuthData, string Name) : ICommand<CustomerDto>;