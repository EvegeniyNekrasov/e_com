using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Auth.Commands;
using Server.Application.DTOs;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var token = await _mediator.Send(new LoginCommand(loginDto));
            return Ok(new { token });
        }
        catch
        {
            return Unauthorized();
        }
    }
}
