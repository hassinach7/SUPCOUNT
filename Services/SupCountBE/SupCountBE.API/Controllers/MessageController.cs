using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Message;
using SupCountBE.Application.Queries.Message;

namespace SupCountBE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [ActionName("GetAll")]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _mediator.Send(new GetAllMessageQuery()));
    }
    [HttpGet]
    [ActionName("GetById")]
    [Route("[action]")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await _mediator.Send(new GetMessageByIdQuery(id)));

    }
    [HttpGet]
    [ActionName("GetPrivateMessage")]
    [Route("[action]")]
    public async Task<IActionResult> GetPrivateMessageAsync()
    {       
        return Ok(await _mediator.Send(new GetPrivateMessageQuery()));
    }
    [HttpPost]
    [ActionName("Create")]
    [Route("[action]")]
    public async Task<IActionResult> CreateAsync(CreateMessageCommand model)
    {
        return Ok(await _mediator.Send(model));
    }

    [HttpPost]
    [ActionName("CreatePrivate")]
    [Route("[action]")]
    public async Task<IActionResult> CreatePrivateAsync(CreatePrivateMessageCommand model)
    {
        await _mediator.Send(model);
        return NoContent();
    }

    [HttpPut]
    [ActionName("Edit")]
    [Route("[action]")]
    public async Task<IActionResult> EditAsync(UpdateMessageCommand model)
    {
        return Ok(await _mediator.Send(model));
    }
}
