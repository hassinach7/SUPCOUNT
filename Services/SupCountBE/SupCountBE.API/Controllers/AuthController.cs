using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Security;
using SupCountBE.Application.Commands.User;
using SupCountBE.Application.Queries.User;
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
        if (command.GoogleAuth)
        {
            var user = await mediator.Send(new GetUserByEmailQuery(command.UserName));
            if(user is null)
            {
                var userId = await mediator.Send(new RegisterUserCommand
                {
                    Password = command.Password,
                    Email = command.UserName,
                    FullName = command.UserName.Split("@")[0],
                    Username = command.UserName
                });
            }
         
        }
        var authModel = await mediator.Send(command);
        if (!authModel.IsAuthenticated)
            return Unauthorized(authModel.Message);
        return Ok(authModel);
    }
}
