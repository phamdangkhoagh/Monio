using MediatR;
using Microsoft.AspNetCore.Mvc;
using Monio.Application.Features.Users.Login;
using Monio.Application.Features.Users.Register;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Monio.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (
            RegisterCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(new
            {
                UserId = userId,
                Email = email
            });
        }
    }
}
