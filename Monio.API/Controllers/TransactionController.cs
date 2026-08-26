using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monio.Application.Features.Transactions.CreateTransaction;
using Monio.Application.Features.Transactions.DeleteTransaction;
using Monio.Application.Features.Transactions.GetTransactionById;
using Monio.Application.Features.Transactions.GetTransactions;
using Monio.Application.Features.Transactions.UpdateTransaction;

namespace Monio.API.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<IActionResult> Get(
            [FromQuery] GetTransactionsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetTransactionByIdQuery
            {
                Id = id
            };

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("")]
        public async Task<IActionResult> Create(
            [FromBody] CreateTransactionCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpPut("")]
        public async Task<IActionResult> Update(
            UpdateTransactionCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(
                new DeleteTransactionCommand
                {
                    Id = id
                }, cancellationToken);

            return NoContent();
        }
    }
}
