using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monio.Application.Features.Wallets.CreateWallet;
using Monio.Application.Features.Wallets.DeleteWallet;
using Monio.Application.Features.Wallets.GetWallets;
using Monio.Application.Features.Wallets.UpdateWallet;
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

        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateWalletCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateWalletCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            
            var result = await _mediator.Send(command,cancellationToken);
            
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteWalletCommand
            {
                Id = id
            }, cancellationToken);

            return NoContent(); 
        }
    }
}
