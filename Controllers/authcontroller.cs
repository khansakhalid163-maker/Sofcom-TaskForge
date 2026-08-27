using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskForge.General.Auth.Commands;
using TaskForge.Models;

namespace TaskForge.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var newId = await _mediator.Send(new RegisterCommand(request));
                return StatusCode(StatusCodes.Status201Created, new { Id = newId });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("already exists"))
                    return StatusCode(StatusCodes.Status409Conflict, ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _mediator.Send(new LoginCommand(request));
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
        }
    }
}