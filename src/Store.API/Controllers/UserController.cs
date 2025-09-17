namespace Store.API.Controllers;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserDto user,
        CancellationToken ct,
        [FromServices] ICommandHandler<RegisterUserCommand, UserDto> handler)
    {
        var result = await handler.Handle(new(user), ct);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUser(
        [FromBody] UserDto user,
        CancellationToken ct,
        [FromServices] ICommandHandler<UpdateUserCommand, UserDto> handler)
    {
        var result = await handler.Handle(new(user), ct);

        if (result is null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
}