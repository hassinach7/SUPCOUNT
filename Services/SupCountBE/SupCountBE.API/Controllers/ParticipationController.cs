using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Participation;
using SupCountBE.Application.Queries.Participation;

namespace SupCountBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParticipationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ActionName("GetAll")]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _mediator.Send(new GetAllParticipationQuery()));
        }



        [HttpPost]
        [ActionName("Create")]
        [Route("[action]")]
        public async Task<IActionResult> CreateAsync(CreateParticipationCommand model)
        {
            return Ok(await _mediator.Send(model));
        }


        [HttpPut]
        [ActionName("Edit")]
        [Route("[action]")]
        public async Task<IActionResult> EditAsync(UpdateParticipationCommand model)
        {
            return Ok(await _mediator.Send(model));
        }

    }
}
