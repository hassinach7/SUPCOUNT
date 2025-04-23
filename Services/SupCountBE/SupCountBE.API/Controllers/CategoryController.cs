using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Category;
using SupCountBE.Application.Queries.Category;

namespace SupCountBE.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [ActionName("GetAll")]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _mediator.Send(new GetAllCategoryQuery()));
    }

    [HttpGet]
    [ActionName("GetById")]
    [Route("[action]")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
       return Ok(await _mediator.Send(new GetCategoryByIdQuery(id)));

    }
  
    [HttpPost]
    [ActionName("Create")]
    [Route("[action]")]
    public async Task<IActionResult> CreateAsync(CreateCategoryCommand model)
    {
        return Ok(await _mediator.Send(model));
    }

  
    [HttpPut]
    [ActionName("Edit")]
    [Route("[action]")]
    public async Task<IActionResult> EditAsync(UpdateCategoryCommand model)
    {
        return Ok(await _mediator.Send(model));
    }

    
}
