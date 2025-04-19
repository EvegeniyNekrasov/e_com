using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Api.Models;
using Server.Application.Auth.Commands;
using Server.Application.DTOs;
using Server.Application.Users.Commands;

namespace Server.Api.Controllers;

/// <summary>
///  Manage authentication: user login and registration.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator, ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Authenticate the user and return Jwt token. 
    /// </summary>
    /// <param name="loginDto">Email and Password</param>
    /// <param name="ct">Cancelation token</param>
    /// <returns>Valid Jwt Token por protected endpoint</returns>
    [AllowAnonymous]
    [HttpPost("[action]")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginDto loginDto, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var token = await _mediator.Send(new LoginCommand(loginDto), ct);
            return Ok(new TokenResponse(token));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { Message = "Invalid credentials" });
        }
    }

    /// <summary>
    ///  Create new user in the system
    /// </summary>
    /// <param name="cmd">New user data</param>
    /// <param name="ct">cancelation token of petition</param>
    /// <returns>Unique new created user id</returns>
    [AllowAnonymous]
    [HttpPost("[action]")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Register([FromBody] CreateUserCommand cmd, CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var id = await _sender.Send(cmd, ct);
        return Created($"/api/users/{id}", new { id });

    }
}
