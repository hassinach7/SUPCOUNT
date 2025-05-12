using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Reimbursement;
using SupCountBE.Application.Queries.Reimbursement;

namespace SupCountBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReimbursementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReimbursementController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ActionName("GetAll")]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _mediator.Send(new GetAllReimbursementQuery()));
        }
        [HttpGet]
        [ActionName("GetById")]
        [Route("[action]")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _mediator.Send(new GetReimbursementByIdQuery(id)));

        }

        [HttpPost]
        [ActionName("Create")]
        [Route("[action]")]
        public async Task<IActionResult> CreateAsync(CreateReimbursementCommand model)
        {
            return Ok(await _mediator.Send(model));
        }


        [HttpPut]
        [ActionName("Edit")]
        [Route("[action]")]
        public async Task<IActionResult> EditAsync(UpdateReimbursementCommand model)
        {
            return Ok(await _mediator.Send(model));
        }
      
        [HttpGet]
        [ActionName("GenerateReimbursements")]
        [Route("[action]")]
        public async Task<IActionResult> GenerateReimbursementsAsync(int groupId)
        {
            return Ok(await _mediator.Send(new GenerateReimbursementsQuery(groupId)));
        }
    }
}
