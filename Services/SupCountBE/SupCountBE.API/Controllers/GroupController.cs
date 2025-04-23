using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Group;
using SupCountBE.Application.Queries.Group;

namespace SupCountBE.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class GroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    [ActionName("GetAll")]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _mediator.Send(new GetAllGroupQuery()));
    }
    [HttpPost]
    [ActionName("Create")]
    [Route("[action]")]
    public async Task<IActionResult> CreateAsync(CreateGroupCommand model)
    {
        return Ok(await _mediator.Send(model));
    }
    [HttpPut]
    [ActionName("Edit")]
    [Route("[action]")]
    public async Task<IActionResult> EditAsync(UpdateGroupCommand model)
    {
        return Ok(await _mediator.Send(model));
    }
}