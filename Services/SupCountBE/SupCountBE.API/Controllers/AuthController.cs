using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Security;
using System.Security.Claims;

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

    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var result = await HttpContext.AuthenticateAsync("Google");

        if (!result.Succeeded)
            return Unauthorized();

        var claims = result.Principal.Identities.First().Claims;
        var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(email))
            return BadRequest("Email not found");

        var command = new GoogleAuthCommand
        {
            Email = email,
            FullName = name ?? email
        };

        var response = await mediator.Send(command);

        if (!response.IsAuthenticated)
            return Unauthorized(response.Message);

        return Ok(response);
    }

}
