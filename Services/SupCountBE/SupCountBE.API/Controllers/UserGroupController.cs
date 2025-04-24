using MediatR;
using Microsoft.AspNetCore.Mvc;
using SupCountBE.Application.Commands.UserGroup;
using SupCountBE.Application.Queries.UserGroup;

namespace SupCountBE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ActionName("GetAll")]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _mediator.Send(new GetAllUserGroupQuery()));
        }
        [HttpGet]
        [ActionName("GetById")]
        [Route("[action]")]
        public async Task<IActionResult> GetByIdAsync(int groupId)
        {
            return Ok(await _mediator.Send(new GetUserGroupByIdQuery(groupId)));

        }

        [HttpPost]
        [ActionName("Create")]
        [Route("[action]")]
        public async Task<IActionResult> CreateAsync(CreateUserGroupCommand model)
        {
            return Ok(await _mediator.Send(model));
        }


        [HttpPut]
        [ActionName("Edit")]
        [Route("[action]")]
        public async Task<IActionResult> EditAsync(UpdateUserGroupCommand model)
        {
            return Ok(await _mediator.Send(model));
        }

    }
}
