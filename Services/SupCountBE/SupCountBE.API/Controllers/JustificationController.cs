using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Justification;
using SupCountBE.Application.Queries.Justification;

namespace SupCountBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JustificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JustificationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ActionName("GetAll")]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync(int expenseId)
        {
            return Ok(await _mediator.Send(new GetAllJustificationQuery(expenseId)));
        }
        [HttpGet]
        [ActionName("GetById")]
        [Route("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _mediator.Send(new GetJustificationByIdQuery(id)));

        }

        [HttpPost]
        [ActionName("Create")]
        [Route("[action]")]
        public async Task<IActionResult> CreateAsync(CreateJustificationCommand model)
        {
            return Ok(await _mediator.Send(model));
        }


        [HttpPut]
        [ActionName("Edit")]
        [Route("[action]")]
        public async Task<IActionResult> EditAsync(UpdateJustificationCommand model)
        {
            return Ok(await _mediator.Send(model));
        }

    }
}
