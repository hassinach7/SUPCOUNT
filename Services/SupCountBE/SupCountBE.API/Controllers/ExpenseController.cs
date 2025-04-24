using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Expense;
using SupCountBE.Application.Queries.Expense;
using SupCountBE.Application.Queries.Participation;

namespace SupCountBE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    [ActionName("GetAll")]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _mediator.Send(new GetAllExpenseQuery()));
    }
    [HttpGet]
    [ActionName("GetById")]
    [Route("[action]")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await _mediator.Send(new GetExpenseByIdQuery(id)));

    }
    [HttpPost]
    [ActionName("Create")]
    [Route("[action]")]
    public async Task<IActionResult> CreateAsync(CreateExpenseCommand model)
    {
        return Ok(await _mediator.Send(model));
    }
    [HttpPut]
    [ActionName("Edit")]
    [Route("[action]")]
    public async Task<IActionResult> EditAsync(UpdateExpenseCommand model)
    {
        return Ok(await _mediator.Send(model));
    }
}
