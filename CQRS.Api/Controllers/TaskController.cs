using CQRS.Application.DTOs;
using CQRS.Infrastructure.Commands;
using CQRS.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskItemDto>> Get()
        {
            return await _mediator.Send(new GetAllTaskQuery());
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskItemDto>> GetById(int id)
        {
            var taskItem = await _mediator.Send(new GetTaskByIdQuery(id));
            if (taskItem is null) NotFound();
            return Ok(taskItem);
        }
        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> Create(CreateTaskCommand command)
        {
            var taskItem = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = taskItem.Id }, taskItem);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateTaskCommand command)
        {
            if (id != command.id) return BadRequest();
            var taskItem = await _mediator.Send(command);
            if (taskItem is null) return NotFound();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var taskItem = await GetById(id);
            if (taskItem is null) return NotFound();
            await _mediator.Send(new DeleteTaskCommand(id));
            return NoContent();
        }
    }
}
