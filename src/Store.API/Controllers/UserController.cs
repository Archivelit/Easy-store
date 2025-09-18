﻿namespace Store.API.Controllers;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserDto user,
        CancellationToken ct)
    {
        var command = new RegisterUserCommand(user);
        
        var result = await _mediator.Send(command, ct);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUser(
        [FromBody] UserDto user,
        CancellationToken ct)
    {
        var command = new UpdateUserCommand(user);

        var result = await _mediator.Send(command, ct);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}