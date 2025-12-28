namespace Store.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        throw new NotImplementedException();

        // Implementation for token retrieval would go here
    }

    /// <summary>
    /// Register a new user in the system
    /// </summary>
    /// <returns>
    /// Registered user model
    /// </returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto userDto, CancellationToken ct)
    {
        var command = new RegisterUserCommand(userDto);

        var registeredUser = await _mediator.Send(command, ct);

        if (registeredUser is null)
            return BadRequest();

        return Ok(registeredUser);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        throw new NotImplementedException();
        
        // Implementation for user logout would go here
    }

    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        throw new NotImplementedException();
        
        // Implementation for token refresh would go here
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        throw new NotImplementedException();

        // Implementation for retrieving current user info would go here
    }
}
