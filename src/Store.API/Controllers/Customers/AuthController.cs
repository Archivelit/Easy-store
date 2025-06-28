using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Contracts.CQRS.Customers.Commands;
using Store.Infrastructure.Entities;

namespace Store.API.Controllers.Customers;

[Route("api/customers")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator) => _mediator = mediator;

    // POST: api/customers/ItemController
    //
    // Registers customer and returns nothing
    //
    // Params:
    //  Name - customer name
    //  Email - customer email
    //  Password - customer password
    [HttpPost("register")]
    public async Task<ActionResult<CustomerEntity>> RegisterAsync(RegisterCustomerCommand command)
    {
        var customer = await _mediator.Send(command);
        return Created();
    }
    
    // POST: api/customers/authenticate
    //
    // Authenticates customer and returns a JWT token if successful
    //
    // Params:
    //  Name - customer name
    //  Password - customer password
    // Return:
    //  Jwt token
    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> AuthenticateAsync(AuthenticateCustomerCommand command) =>
        Ok(await _mediator.Send(command));
}