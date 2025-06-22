using Microsoft.AspNetCore.Mvc;
using Store.API.DTO;
using Store.Core.Contracts.Customers;
using Store.Core.Models;

namespace Store.API.Controllers.Customers;

[Route("api/customers")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ICustomerManager  _customerManager;

    public AuthController(ICustomerManager customerManager)
    {
        _customerManager = customerManager;
    }
    
    // POST: api/customers/register
    [HttpPost("register")]
    public async Task<ActionResult<CustomerEntity>> RegisterAsync(RegisterCustomerRequest model)
    {
        await _customerManager.RegisterAsync(model.Name, model.Email, model.Password);

        return Created();
    }
    
    // POST: api/customers/authenticate
    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> AuthenticateAsync(AuthenticateCustomerRequest request) =>
        Ok(await _customerManager.AuthenticateAsync(request.Email, request.Password));
}