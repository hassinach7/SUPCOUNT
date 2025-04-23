using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.Transaction;
using SupCountBE.Application.Queries.Transaction;

namespace SupCountBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ActionName("GetAll")]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _mediator.Send(new GetAllTransactionQuery()));
        }



        [HttpPost]
        [ActionName("Create")]
        [Route("[action]")]
        public async Task<IActionResult> CreateAsync(CreateTransactionCommand model)
        {
            return Ok(await _mediator.Send(model));
        }


        [HttpPut]
        [ActionName("Edit")]
        [Route("[action]")]
        public async Task<IActionResult> EditAsync(UpdateTransactionCommand model)
        {
            return Ok(await _mediator.Send(model));
        }

    }
}
