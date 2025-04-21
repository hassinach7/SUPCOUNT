using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Expense;
using SupCountBE.Application.Queries.Expense;

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
    public async Task<IActionResult> GetAllAsyn()
    {
        return Ok(await _mediator.Send(new GetAllExpenseQuery()));
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
