
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Users.Commands;
using Server.Application.Users.Queries;
using Server.Domain.Entities;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers(CancellationToken ct)
    {
        var usders = await _sender.Send(new GetUserQuery(), ct);
        return Ok(usders);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<Guid>> CreateUser([FromBody] CreateUserCommand cmd, CancellationToken ct)
    {
        var id = await _sender.Send(cmd, ct);
        return Created($"/api/users/{id}", new { id });
    }
}
