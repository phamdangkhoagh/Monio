using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monio.Application.Features.Wallets.GetWallets;
using Monio.Application.Interfaces.Services;

namespace Monio.API.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WalletController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> GetWallets (CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetWalletsQuery(), cancellationToken);

            return Ok(result);
        }
    }
}
