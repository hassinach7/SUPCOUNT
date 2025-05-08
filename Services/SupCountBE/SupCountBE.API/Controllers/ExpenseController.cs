using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Expense;
using SupCountBE.Application.Commands.Justification;
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
    public async Task<IActionResult> CreateAsync([FromForm] CreateExpenseCommand model, [FromForm] List<IFormFile> files)
    {
        // Send the model to the mediator to handle the expense creation
        int expenseId = await _mediator.Send(model);

        // If files were uploaded, send them to the mediator for processing
        if (files != null && files.Count > 0)
        {

            foreach (var file in files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Copy the file content into the memory stream
                    await file.CopyToAsync(memoryStream);

                    // Get the byte array of the file content
                    byte[] fileBytes = memoryStream.ToArray();
                    // Send the file content to the mediator to create a justification
                    await _mediator.Send(new CreateJustificationCommand
                    {
                        ExpenseId = expenseId,
                        FileContent = fileBytes
                    });
                }
            }
        }

        return Ok(await _mediator.Send(new GetExpenseByIdQuery(expenseId)));
    }

    [HttpPut]
    [ActionName("Edit")]
    [Route("[action]")]
    public async Task<IActionResult> EditAsync(UpdateExpenseCommand model)
    {
        await _mediator.Send(model);
        return NoContent();
    }

    [HttpGet]
    [ActionName("GetAllExpenseByGroup")]
    [Route("[action]")]
    public async Task<IActionResult> GetAllExpenseByGroupAsync(int groupId)
    {
        var result  = await _mediator.Send(new GetAllExpenseByGroupQuery { GroupId = groupId });
    }
}
