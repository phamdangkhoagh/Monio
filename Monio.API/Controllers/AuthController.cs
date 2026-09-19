using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monio.Application.Features.Users.Login;
using Monio.Application.Features.Users.RefreshToken;
using Monio.Application.Features.Users.Register;
using Monio.Application.Interfaces.Services;
using System.Security.Claims;
namespace Monio.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;


        public AuthController (
            IMediator mediator,
            ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
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
            var userId = _currentUserService.UserId;
            var email = _currentUserService.Email;

            return Ok(new
            {
                UserId = userId,
                Email = email
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(
            RefreshTokenCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                command,
                cancellationToken);

            return Ok(result);
        }
    }
}
