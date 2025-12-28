namespace Store.API.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Update user data
    /// </summary>
    /// <returns>
    /// Updated user model
    /// </returns>
    [HttpPatch]
    public async Task<IActionResult> UpdateUser(
        [FromBody] UpdateUserDto user,
        CancellationToken ct)
    {
        var command = new UpdateUserCommand(user);

        var result = await _mediator.Send(command, ct);

        if (result is null)
            return BadRequest();

        return Ok(result);
    }
}