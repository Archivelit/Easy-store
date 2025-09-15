namespace Store.API.Controllers;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> RegisterUser(
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

    [HttpPost]
    public async Task<ActionResult<UserDto>> UpdateUser(
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