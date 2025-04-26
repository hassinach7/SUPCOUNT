using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.User;

namespace SupCountBE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpPost]
    [ActionName("register")]
    [Route("action")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        return Ok(await mediator.Send(command));
    }
}
