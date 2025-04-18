using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> GetAllAsyn()
    {
        return Ok(await _mediator.Send(new GetAllExpenseQuery()));
    }
}
