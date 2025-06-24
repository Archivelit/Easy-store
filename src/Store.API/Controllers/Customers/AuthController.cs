using Microsoft.AspNetCore.Mvc;
using Store.Core.Models.DTO.Customers;
using Store.Core.Contracts.Customers;
using Store.Infrastructure.Entities;

namespace Store.API.Controllers.Customers;

[Route("api/customers")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ICustomerManager  _customerManager;

    public AuthController(ICustomerManager customerManager) =>
        _customerManager = customerManager;
    
    // POST: api/customers/ItemController
    //
    // Registers customer and returns nothing
    //
    // Params:
    //  Name - customer name
    //  Email - customer email
    //  Password - customer password
    [HttpPost("ItemController")]
    public async Task<ActionResult<CustomerEntity>> RegisterAsync(RegisterCustomerRequest model)
    {
        await _customerManager.RegisterAsync(model.Name, model.Email, model.Password);

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
    public async Task<ActionResult<string>> AuthenticateAsync(AuthenticateCustomerRequest request) =>
        Ok(await _customerManager.AuthenticateAsync(request.Email, request.Password));
}