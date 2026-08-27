using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskForge.General.TaskManagement.Commands;
using TaskForge.General.TaskManagement.Queries;
using TaskForge.Models;

namespace TaskForge.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] AddTaskRequest request)
        {
            try
            {
                var newId = await _mediator.Send(new AddTaskCommand(request));
                return StatusCode(StatusCodes.Status201Created, new { Id = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery(id));
            if (result == null)
                return NotFound("Task not found.");

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var results = await _mediator.Send(new GetAllTasksQuery());
            return Ok(results);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskRequest request)
        {
            try
            {
                var success = await _mediator.Send(new UpdateTaskCommand(request));
                return success ? Ok("Task updated.") : NotFound("Task not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _mediator.Send(new DeleteTaskCommand(id));
            return success ? Ok("Task deleted.") : NotFound("Task not found.");
        }
    }
}