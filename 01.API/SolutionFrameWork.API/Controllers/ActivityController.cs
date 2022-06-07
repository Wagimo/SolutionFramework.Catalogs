using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolutionFramework.Application.Features.Activities.Command.Create;
using SolutionFramework.Application.Features.Activities.Command.Delete;
using SolutionFramework.Application.Features.Activities.Command.Update;
using SolutionFramework.Application.Features.Activities.Queries.GetAll;
using SolutionFramework.Application.Features.Activities.Queries.GetById;

namespace SolutionFrameWork.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("activities")]
        public async Task<IActionResult> Activities()
        {
            var result = await _mediator.Send(new GetAllActivitiesCommand { PageSize = 10, CurrentPage=1});
            return Ok(result);
        }

        [HttpPost("GetAllPagin")]
        public async Task<IActionResult> GetAllPagin(GetAllActivitiesCommand data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetByIdActivityCommand() { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateActivityCommand data)
        {
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateActivityCommand data)
        {
            data.Id = id;
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteActivityCommand() { Id = id });
            return Ok(result);
        }
    }
}
