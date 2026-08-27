using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskForge.General.ProjectManagement.Commands;
using TaskForge.General.ProjectManagement.Queries;
using TaskForge.Models;

namespace TaskForge.Controllers.V1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] AddProjectRequest request)
        {
            try
            {
                var newId = await _mediator.Send(new AddProjectCommand(request));
                return StatusCode(StatusCodes.Status201Created, new { Id = newId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery(id));
            if (result == null)
                return NotFound("Project not found.");

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var results = await _mediator.Send(new GetAllProjectsQuery());
            return Ok(results);
        }
    }
}