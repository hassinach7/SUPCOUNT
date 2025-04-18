using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Security;

namespace SupCountBE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;

    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginAuthCommand command)
    {
        var authModel = await mediator.Send(command);
        if (!authModel.IsAuthenticated)
            return Unauthorized(authModel.Message);
        return Ok(authModel);
    }
}
