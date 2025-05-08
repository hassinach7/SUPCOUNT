using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using SupCountBE.Application.Commands.Expense;
using SupCountBE.Application.Commands.Justification;
using SupCountBE.Application.Queries.Expense;
using SupCountBE.Application.Queries.Participation;
using System.Globalization;
using System.Text;

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
    [Route("GenerateExpensePdf")]
    public async Task<IActionResult> GenerateExpensePdfAsync(int groupId)
    {
        var result = await _mediator.Send(new GetAllExpenseByGroupQuery { GroupId = groupId });

        var stream = new MemoryStream();
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Header().Text($"Expenses for Group {groupId}").FontSize(18).Bold().AlignCenter();

                page.Content().Table(table =>
                {
                    // Define columns
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1); // ID
                        columns.RelativeColumn(2); // Title
                        columns.RelativeColumn(1); // Amount
                        columns.RelativeColumn(2); // Date
                        columns.RelativeColumn(2); // Group Name
                        columns.RelativeColumn(2); // Category
                    });

                    // Table header
                    table.Header(header =>
                    {
                        header.Cell().Text("ID").Bold();
                        header.Cell().Text("Title").Bold();
                        header.Cell().Text("Amount").Bold();
                        header.Cell().Text("Date").Bold();
                        header.Cell().Text("Group").Bold();
                        header.Cell().Text("Category").Bold();
                    });

                    // Table rows
                    foreach (var exp in result)
                    {
                        table.Cell().Text(exp.Id.ToString());
                        table.Cell().Text(exp.Title);
                        table.Cell().Text($"{exp.Amount.ToString("C", CultureInfo.InvariantCulture)}");
                        table.Cell().Text(exp.Date.ToString("yyyy-MM-dd"));
                        table.Cell().Text(exp.Group?.Name ?? "N/A");
                        table.Cell().Text(exp.CategoryName ?? "N/A");
                    }
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Generated on ");
                    x.Span(DateTime.Now.ToString("g"));
                });
            });
        })
        .GeneratePdf(stream);

        stream.Position = 0;

        return File(stream, "application/pdf", $"Expenses_Group_{groupId}.pdf");
    }

    [HttpGet]
    [Route("ExportExpensesCsv")]
    public async Task<IActionResult> ExportExpensesCsvAsync(int groupId)
    {
        var result = await _mediator.Send(new GetAllExpenseByGroupQuery { GroupId = groupId });

        var csv = new StringBuilder();

        // Header row
        csv.AppendLine("ID,Title,Amount,Date,CreatedAt,Group,Category,ParticipationCount,JustificationCount,Payer");

        // Data rows
        foreach (var exp in result)
        {
            var row = string.Join(",",
                exp.Id,
                EscapeCsv(exp.Title),
                exp.Amount.ToString(CultureInfo.InvariantCulture),
                exp.Date.ToString("yyyy-MM-dd"),
                exp.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                EscapeCsv(exp.Group?.Name ?? ""),
                EscapeCsv(exp.CategoryName),
                exp.ParticipationCount,
                exp.JustificationCount,
                EscapeCsv(exp.Payer)
            );

            csv.AppendLine(row);
        }

        var byteArray = Encoding.UTF8.GetBytes(csv.ToString());
        var stream = new MemoryStream(byteArray);

        return File(stream, "text/csv", $"Expenses_Group_{groupId}.csv");
    }

    private static string EscapeCsv(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return "";

        return $"\"{value.Replace("\"", "\"\"")}\"";
    }

}
