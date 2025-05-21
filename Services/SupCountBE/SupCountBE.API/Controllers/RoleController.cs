using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Queries.Role;

namespace SupCountBE.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController (IMediator mediator) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetListRoleAsync()
    {
        var result = await mediator.Send(new GetListRoleQuery());
        return Ok(result);
    }
}
