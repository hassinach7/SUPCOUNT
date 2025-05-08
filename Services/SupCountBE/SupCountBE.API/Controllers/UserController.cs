using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.User;
using SupCountBE.Application.Queries.User;

namespace SupCountBE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [ActionName("register")]
    [Route("[action]")]
    public async Task<IActionResult> Register(RegisterUserCommand model)
    {
        return Ok(await _mediator.Send(model));
    }
    [HttpGet]
    [ActionName("GetAll")]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _mediator.Send(new GetAllUserQuery()));
    }
    [HttpGet]
    [ActionName("GetById")]
    [Route("[action]")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        return Ok(await _mediator.Send(new GetUserByIdQuery(id)));
    }
    [HttpPut]
    [ActionName("Edit")]
    [Route("[action]")]
    public async Task<IActionResult> EditAsync(UpdateUserCommand model)
    {
        return Ok(await _mediator.Send(model));
    }
    [HttpGet]
    [ActionName("GetAllUserSoldeByGroupId")]
    [Route("[action]")]
    public async Task<IActionResult> GetAllUserSoldeByGroupIdAsync(int groupId)
    {
        return Ok(await _mediator.Send(new GetAllUserSoldeByGroupIdQuery { GroupId = groupId }));
    }
}
